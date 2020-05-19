using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using JSONImporter.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Confluent.Kafka;
using System.Threading.Tasks;
using System.Threading;
using ProtoBuf;

namespace JSONImporter
{
    [MinColumn, MaxColumn]
    public class Program
    {

        public static string path = @"team.json";// path to the json file
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static List<Team> jsonList = new List<Team>();//list to import data from the json file
        public static Program method = new Program();


        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("1 - Создать JSON файл, 2 - импортировать JSON файл, 3 - провести измерение производительности, 4 - импорт в БД, 5 - добавить сообщение в Kafka, 6 - начать получать сообщения из Kafka");
                    var a = Convert.ToInt32(Console.ReadLine());
                    switch (a)
                    {
                        case 1:
                            bool t = method.CreateJson();
                            if (t)
                                Console.WriteLine("Файл успешно создан");
                            break;
                        case 2:
                            List<Team> list = method.ImportJson();
                            if (list.Count != 0)
                                Console.WriteLine("Данные успешно импортированы в список");
                            break;
                        case 3:
                            BenchmarkRunner.Run<Program>();
                            break;
                        case 4:
                            method.SaveInDB();
                            break;
                        case 5:
                            method.Producer();
                            break;
                        case 6:
                            method.Consumer();
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                logger.Error("При выборе действия вылетело исключение " + e);
            }

        }

        public async Task Producer()
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            jsonList = method.ImportJson();
            using (var p = new ProducerBuilder<Null, Byte[]>(config).Build())
            {
                try
                {
                    foreach(var j in jsonList)
                    {
                            byte[] message = Team.ProtoSerialize<Team>(j);
                            var dr = await p.ProduceAsync("test-topic2", new Message<Null, Byte[]> { Value = message });
                            Console.WriteLine($"Отправлено '{dr.Value}' в '{dr.TopicPartitionOffset}'");
                            break;
                        
                    }
                        
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Отправка провалена: {e.Error.Reason}");
                    logger.Error("При отправке сообщения вылетело исключение " + e);
                }
            }
        }


        public void Consumer()
        {
            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group2",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var c = new ConsumerBuilder<Ignore, byte[]>(conf).Build())
            {
                c.Subscribe("test-topic2");

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) => {
                    e.Cancel = true;
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var topic = c.Consume(cts.Token).TopicPartitionOffset;
                            var message = c.Consume(cts.Token).Message.Value;
                            Team crd = Team.ProtoDeserialize<Team>(message);
                            foreach (var t in crd.teams)
                            {
                                Console.WriteLine($"Получено сообщение '{t.teamNumber}' из: '{topic}'.");
                                break;
                            }

                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    c.Close();
                }
            }
        }

        public void SaveInDB()
        {
            try
            {
                DBContext db = new DBContext();
                jsonList = method.ImportJson();
                var players = new List<int>();
                var teams = new TeamsDBModel();
                var teamsList = new List<int>();
                
                foreach (var list in jsonList)
                {
                    foreach (var inList in list.teams)
                    {

                        db.Coach.Add(inList.coach);

                        db.Details.Add(inList.detail);
                        db.SaveChanges();

                        foreach (var player in inList.players)
                        {
                            players.Add(player.personId);
                            db.Player.Add(player);
                        }
                        teams = new TeamsDBModel
                        {
                            teamNumber = inList.teamNumber,
                            detail = inList.detail.detailId,
                            players = players.ToArray(),
                            coach = inList.coach.personId
                        };
                        teamsList.Add(teams.teamNumber);
                        db.Teams.Add(teams);

                    }
                    var team = new TeamDBModel
                    {
                        messageId = list.messageId,
                        teams = teamsList.ToArray()
                    };
                    db.Team.Add(team);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                logger.Error("При импорте в БД вылетело исключение " + e);
            }
            
        }

        [Benchmark]
        public bool CreateJson() //method that generates the json file
        {
            try
            {
                var players = new List<Player>();
                for (int i = 1; i <= 15000; i++)
                {
                    var player = new Player
                    {
                        pno = i,
                        personId = i,
                        familyName = "Lucas",
                        firstName = "Sarah",
                        shirtNumber = "12",
                        playingPosition = "2",
                        starter = false,
                        captain = false,
                        nationality = "German"
                    };
                    players.Add(player);
                }
                var coach = new Coach
                {
                    personId = 1,
                    familyName = "Barstow",
                    firstName = "Tom",
                    internationalFamilyName = "Barstow",
                    internationalFirstName = "Tom",
                    scoreBoardName = "Tom",
                    TVName = "Tom",
                    nickName = "TomBar",
                    externalId = "1",
                    nationalityCode = "1",
                    nationalityCodeIOC = "1",
                    nationality = "Brazil"
                };

                var detail = new Detail
                {
                    detailId = 2,
                    teamName = "Southern Knights",
                    teamCode = "SKN",
                    teamNickname = "Knights",
                    teamId = "36348"
                };
                var tempTeams = new Teams
                {
                    teamNumber = 1,
                    detail = detail,
                    players = players,
                    coach = coach
                };
                List<Teams> teams = new List<Teams> { tempTeams };
                var tempTeam = new Team
                {
                    messageId = 1,
                    teams = teams
                };
                List<Team> team = new List<Team> { tempTeam };
                string json = JsonConvert.SerializeObject(team);
                File.WriteAllText(path, json);
                logger.Info("Успешно создан новый json файл");
                if (File.Exists(path) == true)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                logger.Error("При создании json файла вылетело исключение " + e);
                return false;
            }
        }

        [Benchmark]
        public List<Team> ImportJson()//method that imports the file
        {
            try
            {
                var jsonFile = File.ReadAllText(path);
                //dynamic jsonList = JsonConvert.DeserializeObject<List<dynamic>>(jsonFile);
                jsonList = JsonConvert.DeserializeObject<List<Team>>(jsonFile);
                //foreach(dynamic f in jsonList)
                //Console.WriteLine(f);
                logger.Info("Json файл успешно импортирован в список");
                Console.WriteLine("Данные успешно импортированы в список");
                return jsonList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                logger.Error("При импорте json файла вылетело исключение " + e);
                return jsonList;
            }
        }
    }
}
