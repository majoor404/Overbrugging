using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace Melding
{
    public partial class FormMelding : Form
    {
        private Vector2 startPosition;
        private Vector2 endPosition;
        private readonly Timer animationTimer;
        private float animationProgress = 0; // Value between 0 and 1
        private Timer wachtTimer;

        private enum StatusForm { start, show, eind};
        public enum Type { Info, Cal, Err, Klaar, Edit, Save , Note};

        public FormMelding()
        {
            InitializeComponent();

            //animationTimer = new Timer
            //{
            //    Interval = 16 // Roughly 60 FPS
            //};
            //animationTimer.Tick += AnimationTimer_Tick;
        }

        //private void AnimationTimer_Tick(object sender, EventArgs e)
        //{
        //    animationProgress += 0.01f; // Adjust for speed
        //    if (animationProgress >= 1)
        //    {
        //        animationProgress = 1;
        //        animationTimer.Stop();
        //        Close(); // Close the form when animation is complete
        //    }

        //    // Ease-in-out interpolatie (S-curve)
        //    float t = animationProgress;
        //    t = t * t * (3f - (2f * t)); // Smoothstep formule

        //    float x = startPosition.X + ((endPosition.X - startPosition.X) * t);
        //    float y = startPosition.Y + ((endPosition.Y - startPosition.Y) * t);

        //    Location = new Point((int)x, (int)y);

        //}

        public void Show(Type type, string regel1, string regel2)
        {

            int ourScreenWidth = Screen.FromControl(this).WorkingArea.Width;
            int ourScreenHeight = Screen.FromControl(this).WorkingArea.Height;
            startPosition = new Vector2(Location.X, Location.Y); // center screen
            endPosition = new Vector2(ourScreenWidth, ourScreenHeight);
            Location = new Point((int)startPosition.X, (int)startPosition.Y);

            label1.Text = regel1;
            label2.Text = regel2;

            switch (type)
            {
                case Type.Info:
                    pictureBox.Image = Overbrugging.Properties.Resources.info;
                    panelColor.BackColor = Color.FromArgb(0, 147, 241);
                    break;
                case Type.Cal:
                    pictureBox.Image = Overbrugging.Properties.Resources.calendar;
                    panelColor.BackColor = Color.FromArgb(226, 209, 203);
                    break;
                case Type.Err:
                    pictureBox.Image = Overbrugging.Properties.Resources.caution;
                    panelColor.BackColor = Color.FromArgb(255, 179, 32);
                    break;
                case Type.Klaar:
                    pictureBox.Image = Overbrugging.Properties.Resources.finish;
                    panelColor.BackColor = Color.FromArgb(80, 56, 55);
                    break;
                case Type.Edit:
                    pictureBox.Image = Overbrugging.Properties.Resources.pencil;
                    panelColor.BackColor = Color.FromArgb(169, 204, 102);
                    break;
                case Type.Save:
                    pictureBox.Image = Overbrugging.Properties.Resources.usb;
                    panelColor.BackColor = Color.FromArgb(80, 203, 203);
                    break;
                case Type.Note:
                    pictureBox.Image = Overbrugging.Properties.Resources.notes;
                    panelColor.BackColor = Color.FromArgb(255, 189, 66);
                    break;
                default:
                    pictureBox.Image = Overbrugging.Properties.Resources.info;
                    panelColor.BackColor = Color.FromArgb(0, 147, 241);
                    break;
            }
            this.Show();
            wachtTimer = new Timer();
            wachtTimer.Interval = 1500; // 1 seconde
            wachtTimer.Tick += WachtTimer_Tick;
            wachtTimer.Start();
        }

        private void WachtTimer_Tick(object sender, EventArgs e)
        {
            wachtTimer.Stop();
            wachtTimer.Tick -= WachtTimer_Tick;
            wachtTimer.Dispose();
            wachtTimer = null;

            Close();
            //animationProgress = 0; // Reset animation
            //animationTimer.Start();
        }

        private void FormMelding_MouseClick(object sender, MouseEventArgs e)
        {
            Close();
        }
    }
}
