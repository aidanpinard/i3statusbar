using System;

using Newtonsoft.Json;

namespace i3statusbar.Types
{
    [JsonConverter(typeof(ColourJsonConverter))]
    public class Colour
    {
        public class ColourJsonConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return (objectType == typeof(Colour));
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteValue(value.ToString());
            }

            public override bool CanRead
            {
                get { return false; }
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }

        public static Colour Black {
            get
            {
                return new Colour(0);
            }
        }

        private int _code = 0;

        public int Red {
            get {
                return (_code >> 16) & 0xff;
            }
            set {
                if (0x00 <= value && value <= 0xFF)
                {
                    _code &= ~(0xff << 16);
                    _code |= (value << 16) & (0xff << 16);
                }
            }
        }

        public int Green {
            get {
                return (_code >> 8) & 0xff;
            }
            set {
                if (0x00 <= value && value <= 0xFF)
                {
                    _code &= ~(0xff << 8);
                    _code |= (value << 8) & (0xff << 8);
                }
            }
        }

        public int Blue {
            get {
                return _code & 0xff;
            }
            set {
                if (0x00 <= value && value <= 0xFF)
                {
                    _code &= ~0xff;
                    _code |= value & 0xff;
                }
            }
        }

        public int Code 
        {
            get 
            {
                return _code;
            }
            set 
            { 
                _code = Math.Clamp(value, 0, 0xffffff);
            }
        }

        public Colour(int red, int green, int blue)
        {
            Red = red;
            Blue = blue;
            Green = green;
        }

        public Colour(int code)
        {
            _code = code;
        }

        public override string ToString()
        {
            return $"#{_code:X6}";
        }
    }
}