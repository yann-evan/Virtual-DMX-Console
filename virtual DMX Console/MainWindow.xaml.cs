using System;
using System.Collections.Generic;
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
using DmxControlLib.Hardware;
using DmxControlLib.Utility;
using DmxUserControlLib;

namespace virtual_DMX_Console
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool LearningMode;
        public Scene current_scene;
        public List<ChannelController> ChControllerList;
        public List<Scene> SavedScene;

        public MainWindow()
        {
            InitializeComponent();

            LearningMode = false;

            current_scene = new Scene("current_scn");
            ChControllerList = new List<ChannelController>();

            SavedScene.Add(new Scene(SCBT01.Name));
            SavedScene.Add(new Scene(SCBT02.Name));
            SavedScene.Add(new Scene(SCBT03.Name));
            SavedScene.Add(new Scene(SCBT04.Name));
            SavedScene.Add(new Scene(SCBT05.Name));
            SavedScene.Add(new Scene(SCBT06.Name));
            SavedScene.Add(new Scene(SCBT07.Name));
            SavedScene.Add(new Scene(SCBT08.Name));
            SavedScene.Add(new Scene(SCBT09.Name));
            SavedScene.Add(new Scene(SCBT10.Name));
            SavedScene.Add(new Scene(SCBT11.Name));
            SavedScene.Add(new Scene(SCBT12.Name));
            SavedScene.Add(new Scene(SCBT13.Name));
            SavedScene.Add(new Scene(SCBT14.Name));
            SavedScene.Add(new Scene(SCBT15.Name));
            SavedScene.Add(new Scene(SCBT16.Name));

            ChControllerList.Add(chControl1);
            ChControllerList.Add(chControl2);
            ChControllerList.Add(chControl3);
            ChControllerList.Add(chControl4);
            ChControllerList.Add(chControl5);
            ChControllerList.Add(chControl6);
            ChControllerList.Add(chControl7);
            ChControllerList.Add(chControl8);
            ChControllerList.Add(chControl9);
            ChControllerList.Add(chControl10);
            ChControllerList.Add(chControl11);
            ChControllerList.Add(chControl12);
            ChControllerList.Add(chControl13);
            ChControllerList.Add(chControl14);
            ChControllerList.Add(chControl15);
            ChControllerList.Add(chControl16);

            for (int i = 0; i < ChControllerList.Count; i++)
            {
                ChControllerList[i].channel = i+1;
            }

            DmxController.Connect();
        }

        private void ChannelController_DmxValue_Changed(object sender, EventArgs e)
        {
            DmxUserControlLib.ChannelController control;

            if(sender is DmxUserControlLib.ChannelController)
            {
                control = sender as DmxUserControlLib.ChannelController;

                current_scene.setDmxValue(control.channel, control.dmxvalue);

                if (DmxController.IsOpen())
                {
                    DmxController.WriteValue(control.channel, control.dmxvalue);
                }
            }
        }

        private void Button_Click_Channel_Down(object sender, RoutedEventArgs e)
        {
            if(chControl1.channel > 1)
            {
                for (int i = 0; i < ChControllerList.Count; i++)
                {
                    ChControllerList[i].channel--;
                }
            }
        }

        private void Button_Click_Channel_Up(object sender, RoutedEventArgs e)
        {

            if(chControl16.channel < 512)
            {
                for (int i = 0; i < ChControllerList.Count; i++)
                {
                    ChControllerList[i].channel++;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(LearningMode)
            {

                Learning_BT.Background = Brushes.LightGray;
                LearningMode = false;
            }
            else
            {
                Learning_BT.Background = Brushes.IndianRed;
                LearningMode = true;
            }
        }

        private void SCBT_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button BuffBT;

            if(sender is System.Windows.Controls.Button)
            {
                BuffBT = sender as System.Windows.Controls.Button;
                if (LearningMode)
                {
                    for (int i = 0; i < SavedScene.Count; i++)
                    {
                        if (SavedScene[i].name == BuffBT.Name)
                        {
                            SavedScene[i] = current_scene;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < SavedScene.Count; i++)
                    {
                        if (SavedScene[i].name == BuffBT.Name)
                        {
                            current_scene = SavedScene[i];
                            break;
                        }
                    }
                }
                
            }
        }
    }
}
