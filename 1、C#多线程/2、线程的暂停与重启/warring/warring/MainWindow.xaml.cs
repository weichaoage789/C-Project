using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace warring
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VoiceWarring.Start(VoiceWarringType.special);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            VoiceWarring.Stop();
        }
    }


    public static class VoiceWarring
    {
        private static ManualResetEvent ma = new ManualResetEvent(true);
        private static VoiceWarringType voiceWarringType = VoiceWarringType.general;
        static VoiceWarring()
        {
            Thread VoiceWarringThread = new Thread(VoiceWarringFunction);
            VoiceWarringThread.Start();
        }

        public static void VoiceWarringFunction()
        {
            while (true)
            {
                //普通报警
                if (voiceWarringType == VoiceWarringType.general)
                {
                    Console.Beep(800, 500);
                    for (int i = 0; i < 5; i++)
                    {
                        ma.WaitOne();
                        Thread.Sleep(1000);
                    }

                   
                }
                //严重报警
                else if (voiceWarringType == VoiceWarringType.special)
                {
                    Console.Beep(800, 500);
                    Thread.Sleep(1000);
                    ma.WaitOne();
                }

            }
        }
        public static void Start(VoiceWarringType type)
        {
            voiceWarringType = type;
            ma.Set();

        }

        public static void Stop()
        {
            ma.Reset();
        }

    }

    public enum VoiceWarringType
    {
        general,  //普通报警
        special   //紧急报警
    }
}
