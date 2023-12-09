﻿using Newtonsoft.Json;

namespace MouseClicker.cs
{
    public class Macro
    {
        public string name;
        private List<TimedPoint> points;

        public Macro()
        {
            this.name = "NewMacro";
            this.points = new List<TimedPoint>();
        }

        public Macro(string name)
        {
            this.name = name;
            this.points = new List<TimedPoint>();
        }

        public void AddPoint(Point point, long millisecondsFromPreviousPoint)
        {
            TimedPoint timedPoint;
            timedPoint.point = point;
            timedPoint.milliseconds = millisecondsFromPreviousPoint;

            this.points.Add(timedPoint);
        }

        public void AddPoint(TimedPoint timedPoint)
        {
            this.points.Add(timedPoint);
        }

        public TimedPoint[] GetPoints()
        {
            return this.points.ToArray();
        }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(points, Formatting.Indented);
        }

        public bool LoadFromJSON(string jsonString)
        {
            try
            {
                if (jsonString is not null)
                {
                    TimedPoint[]? points = JsonConvert.DeserializeObject<TimedPoint[]>(jsonString);
                    if (points is not null)
                    {
                        this.points = points.ToList<TimedPoint>();
                        return true;
                    }

                    return false;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override string ToString()
        {
            string macroDesc = "";

            if(this.points.Count > 0)
            {
                macroDesc += "-------------------------\n";
                int i = 1;

                foreach (TimedPoint timedPoint in this.points)
                {

                    macroDesc += "Point " + i++ + "\n";
                    macroDesc += "X: " + timedPoint.point.X + " - Y: " + timedPoint.point.Y + "\n";
                    macroDesc += "Delay: " + timedPoint.milliseconds + " milliseconds" + "\n";
                    macroDesc += "-------------------------\n";
                }
            }

            return macroDesc;
        }
    }
}
