using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Net.Http;
using System;

namespace GNTL_Bot.Modules
{

    // Modules must be public and inherit from an IModuleBase
    public class CommandsList : ModuleBase<SocketCommandContext>
    {
        static HttpClient hClient = new HttpClient();

        [Command("ping")]
        [Alias("pong", "hello")]
        public Task PingAsync() => ReplyAsync("pong!");


        [Command("userinfo")]
        public async Task UserInfoAsync(IUser user = null)
        {
            user = user ?? Context.User;

            await ReplyAsync(user.ToString());
        }


        [Command("wipe")]
        public async Task GetLog() { 
            LogsResponse lResponse = new LogsResponse();
            List<LogsResponse> list = new List<LogsResponse>();
            HttpResponseMessage response = await hClient.GetAsync(APIKeys.WarcraftLogURL);
            System.DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            System.DateTime endTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);


            if (response.IsSuccessStatusCode) {
               list = await response.Content.ReadAsAsync<List<LogsResponse>>();
            }
            for (int x = 0; x < list.Count(); x++) {
                startTime = Functions.ConvertUnixtoDateTime(list[x].start);
                endTime = Functions.ConvertUnixtoDateTime(list[x].end);
                if (list[x].owner == APIKeys.WarcraftLogOwner && startTime.Day == DateTime.Now.Day) {
                    await ReplyAsync("!wipefest listen --report " + list[x].id + " -death-threshold 5");
                }
                if (list[x].owner == APIKeys.WarcraftLogOwner) { // Comment this on raid day after debugging
                    await ReplyAsync("!wipefest listen --report " + list[x].id + " -death-threshold 5");
                    x = list.Count();
                    return;
                }
            }
        }
    }
}