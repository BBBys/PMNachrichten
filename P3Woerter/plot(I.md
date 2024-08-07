using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

class Program
{
static void Main()
{
string connectionString = "Server=your_server;Database=your_database;User ID=your_user;Password=your_password;";
List<double> xValues = new List<double>();
List<double> yValues = new List<double>();
List<double> zValues = new List<double>();

```
    using (MySqlConnection conn = new MySqlConnection(connectionString))
    {
        conn.Open();
        string query = "SELECT x, y, z FROM your_table";
        MySqlCommand cmd = new MySqlCommand(query, conn);
        using (MySqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                xValues.Add(reader.GetDouble("x"));
                yValues.Add(reader.GetDouble("y"));
                zValues.Add(reader.GetDouble("z"));
            }
        }
    }

    int count = xValues.Count;
    double meanX = CalculateMean(xValues);
    double meanY = CalculateMean(yValues);
    double meanZ = CalculateMean(zValues);
    double stdDevX = CalculateStdDev(xValues, meanX);
    double stdDevY = CalculateStdDev(yValues, meanY);
    double stdDevZ = CalculateStdDev(zValues, meanZ);

    Console.WriteLine($"Anzahl: {count}");
    Console.WriteLine($"Mittelwerte: x = {meanX}, y = {meanY}, z = {meanZ}");
    Console.WriteLine($"Streuung: x = {stdDevX}, y = {stdDevY}, z = {stdDevZ}");

    PlotData(xValues, yValues, zValues);
}

static double CalculateMean(List<double> values)
{
    double sum = 0;
    foreach (var value in values)
    {
        sum += value;
    }
    return sum / values.Count;
}

static double CalculateStdDev(List<double> values, double mean)
{
    double sum = 0;
    foreach (var value in values)
    {
        sum += Math.Pow(value - mean, 2);
    }
    return Math.Sqrt(sum / values.Count);
}

static void PlotData(List<double> xValues, List<double> yValues, List<double> zValues)
{
    var plotModel = new PlotModel { Title = "Plot von y und z gegen x" };

    var ySeries = new LineSeries { Title = "y gegen x", Color = OxyColors.Blue };
    for (int i = 0; i < xValues.Count; i++)
    {
        ySeries.Points.Add(new DataPoint(xValues[i], yValues[i]));
    }

    var zSeries = new LineSeries { Title = "z gegen x", Color = OxyColors.Red };
    for (int i = 0; i < xValues.Count; i++)
    {
        zSeries.Points.Add(new DataPoint(xValues[i], zValues[i]));
    }

    plotModel.Series.Add(ySeries);
    plotModel.Series.Add(zSeries);

    var plotView = new PlotView { Model = plotModel };
    var form = new System.Windows.Forms.Form { ClientSize = new System.Drawing.Size(800, 600) };
    form.Controls.Add(plotView);
    System.Windows.Forms.Application.Run(form);
}
```

}