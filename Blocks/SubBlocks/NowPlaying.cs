using i3statusbar.ClickEvents;

namespace i3statusbar.Blocks.SubBlocks
{
    public class NowPlaying : SubBlock
    {
        private const int MAX_LENGTH = 30;
        private string _currentSong = "";

        private string RotateString(string s, int n)
        {
            return s.Substring(n) + s.Substring(0, n);
        }

        private string GenerateSongString(string artist, string title)
        {
            string song = "";
            if (artist != "")
            {
                song += artist;
            }
            if (title != "")
            {
                if (song != "")
                {
                    song += " - ";
                }
                song += title;
            }
            return song;
        }

        private string GeneratePlayerInfo(string artist, string title)
        {
            string info = GenerateSongString(artist, title);
            
            if (_currentSong != info)
            {
                _currentSong = info;
                if (info.Length > MAX_LENGTH)
                {
                    info = info.Substring(0, MAX_LENGTH) + "...";
                }
            } else {
                info = RotateString(info, 3);
                if (info.Length > MAX_LENGTH)
                {
                    info = info.Substring(0, MAX_LENGTH) + "...";
                }
            }

            return info;
        }

        public NowPlaying(Block parent) : base(parent)
        {
            Instance = nameof(NowPlaying);
        }

        public override void Update()
        {
            string[] info = HelperFunctions.RunCommand("/usr/bin/playerctl", "metadata -a -f \"{{artist}}\\{{title}}\\{{status}}\"")
                .Split('\\');
            if (info.Length == 3)
            {
                char icon = '\uf04b';
                if (info[2] == "Paused")
                {
                    icon = '\uf04c';
                }
                else if (info[2] == "Stopped")
                {
                    icon = '\uf04d';
                }
                FullText = $"{icon} {GeneratePlayerInfo(info[0], info[1])}";
            }
            else
            {
                FullText = "\uf04d";
            }
        }

        public override void ProcessClickEvent(object sender, ClickEventArgs args)
        {
            if (args.Modifiers.Contains("Shift"))
            {
                HelperFunctions.LaunchApplication("/usr/bin/playerctl", "stop");
            }
            else
            {
                HelperFunctions.LaunchApplication("/usr/bin/playerctl", "play-pause");
            }
        }
    }
}