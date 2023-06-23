using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DZ11WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> answers = new List<string>();
        public MainWindow()
        {
            var PathForProject = Directory.GetCurrentDirectory();
            try
            {
                FileInfo fileInfo = new FileInfo(System.IO.Path.Combine(PathForProject, "Answers.txt"));

                using (FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        answers.Add(streamReader.ReadLine());
                    }
                }
                
                for(int i = 0; i < answers.Count; i++)
                {
                    if (answers[i] == "" || answers[i] == null || answers[i] == " ") answers.Remove(answers[i]);
                }
            }
            catch (FileNotFoundException)
            {
                using (FileStream fs = new FileStream("Answers.txt", FileMode.Create))
                {
                    using (StreamWriter SW = new StreamWriter(fs))
                    {
                        SW.Write("[ Yes ]\n");
                        SW.Write("[ No ]\n");
                        SW.Write("[ You should do it ]\n");
                        SW.Write("[ Impossible ]\n");
                        SW.Write("[ It's worth a try ]\n");
                        SW.Write("[ Try do it ]\n");
                        SW.Write("[ Chances are slim ]\n");
                    }
                }

                answers.Add("[ Yes ]");
                answers.Add("[ No ]");
                answers.Add("[ You should do it ]");
                answers.Add("[ Impossible ]");
                answers.Add("[ It's worth a try ]");
                answers.Add("[ Try do it ]");
                answers.Add("[ Chances are slim ]");
            }


            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(TBQ.Text == null || TBQ.Text == "" || TBQ.Text == "[ Enter your question ]")
            { 
                TBA.Text = "[ Enter the question first! ]"; 
            }
            else
            {
                var r = new Random();
                string answer = answers[r.Next(0, answers.Count - 1)];
                TBA.Text = answer;
            }
        }
    }
}
