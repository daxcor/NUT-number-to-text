﻿using System;
using System.Text;

namespace Nut.TextConverters
{
    public sealed partial class TurkishConverter : BaseConverter
    {

        private static readonly Lazy<TurkishConverter> Lazy = new Lazy<TurkishConverter>(() => new TurkishConverter());
        public static TurkishConverter Instance { get { return Lazy.Value; } }
        public TurkishConverter()
        {
            Initialize();
        }
        protected override long Append(long num, long scale, StringBuilder builder)
        {
            if (num > scale - 1)
            {
                var baseScale = num / scale;
                if (!(baseScale == 1 && (scale == 100 || scale == 1000)))
                {
                    AppendLessThanOneThousand(baseScale, builder);
                }

                builder.AppendFormat("{0} ", Scales[scale]);
                num = num - (baseScale * scale);
            }
            return num;
        }

        protected override long AppendTens(long num, StringBuilder builder)
        {
            if (num > 10)
            {
                var tens = num / 10 * 10;
                builder.AppendFormat("{0} ", TextStrings[tens]);
                num = num - tens;
            }
            return num;
        }

        protected override long AppendHundreds(long num, StringBuilder builder)
        {
            if (num > 99)
            {
                var hundreds = num / 100;
                if (hundreds != 1)
                {
                    builder.AppendFormat("{0} {1} ", TextStrings[hundreds], TextStrings[100]);
                }
                else
                {
                    builder.AppendFormat("{0} ", TextStrings[100]);
                }
                num = num - (hundreds * 100);
            }
            return num;
        }

        private void Initialize()
        {
            TextStrings.Add(0, "sıfır");
            TextStrings.Add(1, "bir");
            TextStrings.Add(2, "iki");
            TextStrings.Add(3, "üç");
            TextStrings.Add(4, "dört");
            TextStrings.Add(5, "beş");
            TextStrings.Add(6, "altı");
            TextStrings.Add(7, "yedi");
            TextStrings.Add(8, "sekiz");
            TextStrings.Add(9, "dokuz");
            TextStrings.Add(10, "on");
            TextStrings.Add(20, "yirmi");
            TextStrings.Add(30, "otuz");
            TextStrings.Add(40, "kırk");
            TextStrings.Add(50, "elli");
            TextStrings.Add(60, "altmış");
            TextStrings.Add(70, "yetmiş");
            TextStrings.Add(80, "seksen");
            TextStrings.Add(90, "doksan");
            TextStrings.Add(100, "yüz");

            Scales.Add(1000000000, "milyar");
            Scales.Add(1000000, "milyon");
            Scales.Add(1000, "bin");
        }
    }
}
