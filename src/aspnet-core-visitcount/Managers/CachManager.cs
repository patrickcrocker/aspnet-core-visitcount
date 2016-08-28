﻿using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace aspnet_core_visitcount.Managers
{
    public class CachManager
    {
        ConnectionMultiplexer _redis;
        public CachManager()
        {
            //_redis = ConnectionMultiplexer.Connect("localhost");
            //{"p-redis":[{"credentials":{"host":"redis.local.pcfdev.io","password":"a9f5d25e-39bd-40d3-9326-2ccb7be58b8a","port":36053}
            //"host":"redis.local.pcfdev.io","password":"a9f5d25e-39bd-40d3-9326-2ccb7be58b8a","port":36053}

            var connectionString = getRedisConnString();
            _redis = ConnectionMultiplexer.Connect(connectionString);
        }

        public static string GetIpFromHost(string hostname)
        {
            string ipString = "";
            IPAddress[] ips;

            ips = Dns.GetHostAddressesAsync(hostname).Result;

            Console.WriteLine("GetHostAddresses({0}) returns:", hostname);

            foreach (IPAddress ip in ips)
            {
                if(ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipString = ip.ToString();
                    Console.WriteLine("{0}={1}", hostname, ipString);
                    break;
                }
            }

            return ipString;
        }

        string getRedisConnString()
        {
            string connectionString = string.Empty;
            //[Main.ENV] VCAP_SERVICES [{"p-redis":[{"credentials":{"host":"redis.local.pcfdev.io","password":"a9f5d25e-39bd-40d3-9326-2ccb7be58b8a","port":36053},"syslog_drain_url":null,"label":"p-redis","provider":null,"plan":"shared-vm","name":"core-redis","tags":["pivotal","redis"]}]}]

            string VCAP_SERVICES = Environment.GetEnvironmentVariable("VCAP_SERVICES"); //Set environment variable. check project priperties

            //if(string.IsNullOrWhiteSpace(VCAP_SERVICES) && (Environment.GetEnvironmentVariable("OS") + "").ToLower().IndexOf("win") >= 0)
            //{
            //    VCAP_SERVICES = "{\"p-redis\":[{\"credentials\":{\"host\":\"redis.local.pcfdev.io\",\"password\":\"a9f5d25e-39bd-40d3-9326-2ccb7be58b8a\",\"port\":36053},\"syslog_drain_url\":null,\"label\":\"p-redis\",\"provider\":null,\"plan\":\"shared-vm\",\"name\":\"core-redis\",\"tags\":[\"pivotal\",\"redis\"]}]}";
            //}

            dynamic jsonResponse = JsonConvert.DeserializeObject(VCAP_SERVICES);
            string host = jsonResponse["p-redis"][0]["credentials"].host;
            string password = jsonResponse["p-redis"][0]["credentials"].password;
            string port = jsonResponse["p-redis"][0]["credentials"].port;

            string ip = GetIpFromHost(host);
            string connectionStringFormat = "{0}:{1},password={2}";
            connectionString = string.Format(connectionStringFormat, ip, port, password);

            Console.WriteLine("[redis-connectionString] {0}", connectionString);
            return connectionString;
        }

        public int GetPageVisitCounter(string pageName)
        {
            int returnValue = 0;
            try
            {
                Console.WriteLine("[GetPageVisitCounter] in");

                IDatabase db = _redis.GetDatabase();
                string key = pageName + "-Counter";

                Console.WriteLine("[GetPageVisitCounter] key={0}", key);

                string counterString = db.StringGet(key);
                int.TryParse(counterString, out returnValue);
                returnValue++;
                db.StringSet(key, returnValue.ToString());

                Console.WriteLine("[GetPageVisitCounter] {0}", returnValue);
            }
            catch (Exception ex)
            {
                returnValue = -1;
                Console.WriteLine("[GetPageVisitCounter] Exception");
                Console.WriteLine(ex.ToString());
            }

            return returnValue;
        }
    }
}