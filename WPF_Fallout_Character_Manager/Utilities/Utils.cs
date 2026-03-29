using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Fallout_Character_Manager.Models.Inventory;

namespace WPF_Fallout_Character_Manager.Utilities
{
    // https://stackoverflow.com/questions/17810092/how-to-bind-an-observablecollectionbool-to-a-listbox-of-checkboxes-in-wpf
    public class TypeWrap<T> : INotifyPropertyChanged
    {
        private T value;
        public T Value
        {
            get { return value; }
            set
            {
                {
                    this.value = value;
                    OnPropertyChanged();
                }
            }
        }

        public static implicit operator TypeWrap<T>(T value)
        {
            return new TypeWrap<T> { value = value };
        }
        public static implicit operator T(TypeWrap<T> wrapper)
        {
            return wrapper.value;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    static class Utils
    {
        // https://stackoverflow.com/questions/17252615/get-string-between-two-strings-in-a-string
        public static string Between(string str, string firstString, string lastString)
        {
            if (str == "")
                return str;

            string finalString;
            int pos1 = firstString == "" ? 0 : str.IndexOf(firstString) + firstString.Length;
            int pos2 = lastString == "" ? str.Length : str.IndexOf(lastString);
            finalString = str.Substring(pos1, pos2 - pos1);
            return finalString;
        }

        public static float FloatFromString(string s)
        {
            if (float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
            {
                return result;
            }

            throw new Exception($"Failed to convert {s} to float...");
        }

        public static void ProcessCostString(string costString, out CostType costType, out int cost)
        {
            if (costString.StartsWith("x"))
            {
                string ss = costString.Substring(1);
                costType = CostType.Percentage;
                float value = Utils.FloatFromString(ss);
                cost = (int)(value * 100);
            }
            else
            {
                costType = CostType.Flat;
                cost = Int32.Parse(costString);
            }
        }

        public static bool GetRangeMultiplier(string ranges, out ValueTuple<int, int> result)
        {
            result = default;
            if (!ranges.Contains('/'))
                return false;
            var rangeArr = ranges.Split('/');
            string ss1 = rangeArr[0].Substring(1);
            string ss2 = rangeArr[1].Substring(1);
            result = new ValueTuple<int, int>(Int32.Parse(ss1), Int32.Parse(ss2));
            return true;

        }

        public static void SortObservableCollection<T>(this ObservableCollection<T> collection, Func<T, object> predicate)
        {
            List<T> sorted = collection.OrderBy(predicate).ToList();
            collection.Clear();
            foreach(T item in sorted)
            {
                collection.Add(item);
            }
        }

        public static bool IdFromString(string s, out Guid id)
        {
            id = Guid.NewGuid();
            if (s != "")
            {
                id = Guid.Parse(s);
                return true;
            }

            return false;
        }

        // this will update fill in ids for any Property or Upgrade that didn't have one already.
        public static bool UpdateCSVFilesIdFields(string csvPath)
        {
            // This is a developer method which had a one-time use to generate ids in the csv files.
            // It shouldn't be used in normal circumstances as csv files are not intended to be modified by the user.
            // If you want to use it, comment out this exception and make sure you know what you're doing.
            return false;

            string[] fileLines = File.ReadAllLines(csvPath);

            List<string> columnNames = fileLines[0].Split(';').ToList();
            int idColumnIndex = columnNames.FindIndex(x => x.Equals("id", StringComparison.InvariantCultureIgnoreCase)); ;
            if (idColumnIndex == -1)
            {
                return false;
            }

            for (int i = 1; i < fileLines.Length; i++)
            {
                string[] parts = fileLines[i].Split(';');
                if (parts[idColumnIndex] == "")
                {
                    Utils.IdFromString("", out Guid newId);
                    parts[idColumnIndex] = newId.ToString();
                    fileLines[i] = string.Join(";", parts);
                }
            }

            File.WriteAllLines(csvPath, fileLines);
            return true;
        }

        public static void AddCSVLine(string csvPath, string csvLine)
        {
            string[] addition = { csvLine };
            File.AppendAllLines(csvPath, addition);
        }

        public static TExpected EnsureDtoType<TExpected>(object dto)
        {
            if (dto is not TExpected typedDto)
                throw new InvalidOperationException($"Expected {typeof(TExpected).Name}");

            return typedDto;
        }

        public static void ClampWindowWithinScreen(Window window)
        {
            Point mousePosition = Application.Current.MainWindow.PointToScreen(Mouse.GetPosition(Application.Current.MainWindow));

            // convert mouse from physical pixels to WPF DIPs
            var source = PresentationSource.FromVisual(window);
            if (source?.CompositionTarget != null)
            {
                mousePosition = source.CompositionTarget
                    .TransformFromDevice
                    .Transform(mousePosition);
            }

            Rect workingArea = SystemParameters.WorkArea;
            double left = mousePosition.X;
            double top = mousePosition.Y;

            // right
            if (left + window.ActualWidth > workingArea.Right)
                left = workingArea.Right - window.ActualWidth;

            // bottom
            if (top + window.ActualHeight > workingArea.Bottom)
                top = workingArea.Bottom - window.ActualHeight;

            // left (prevent negative)
            if (left < workingArea.Left)
                left = workingArea.Left;

            // top (prevent negative)
            if (top < workingArea.Top)
                top = workingArea.Top;

            window.Left = left;
            window.Top = top;
        }
    }
}
