﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace WhiskeyEditor.UI.Dockable
{
    public delegate void DockControlDockChangedEventHandler(object sender, DockControlDockChangedEventArgs args);
    public class DockControlDockChangedEventArgs : EventArgs
    {
        public DockControl DockControl { get; private set; }
        public DockControlDockChangedEventArgs(DockControl dockControl)
        {
            DockControl = dockControl;
        }
    }

    public class DockControl : Control
    {
        private Control parent;
        private DockStyle defaultDockStyle;
        private bool viewable;
        private Control primaryView;
        private TableLayoutPanel tablePanel;
        private Panel menuPanel;
        private Button closeBtn;
        private Label label;
        private StatusStrip resizeStrip;
        private bool horizantalSplit;



        public DockControl(Control parent, DockStyle defaultDockStyle)
        {
            this.parent = parent;
            this.defaultDockStyle = defaultDockStyle;
            Size = new Size(200, 100);
            BackColor = Color.Red;

            initControls();
            addControls();
            configureControls();

           
      
        }

        #region events
        public event DockControlDockChangedEventHandler Undocked = new DockControlDockChangedEventHandler( (s, a) => {});
        public event DockControlDockChangedEventHandler Docked = new DockControlDockChangedEventHandler( (s, a) => {});
        public event DockControlDockChangedEventHandler DockedChanged = new DockControlDockChangedEventHandler( (s, a) => {});

        private void fireDockChangedEvt(DockControlDockChangedEventArgs args)
        {
            if (DockedChanged != null)
                DockedChanged(this, args);
        }
        private void fireDockedEvt(DockControlDockChangedEventArgs args)
        {
            if (Docked != null)
            {
                Docked(this, args);
                fireDockChangedEvt(args);
            }
        }
        private void fireUndockedEvt(DockControlDockChangedEventArgs args)
        {
            if (Undocked != null)
            {
                Undocked(this, args);
                fireDockChangedEvt(args);
            }
        }
        #endregion

        public bool Viewable
        {
            get { return this.viewable; }
            set { this.viewable = value; }
        }

        public Control PrimaryView
        {
            get
            {
                return primaryView;
            }
            set
            {
                if (primaryView != null)
                {
                    tablePanel.Controls.Remove(primaryView);
                }

                primaryView = value;
                primaryView.Dock = DockStyle.Fill;
                primaryView.Padding = new Padding(0);
                primaryView.Margin = new Padding(0);
                label.Text = primaryView.Name;
                tablePanel.Controls.Add(primaryView, 0, 1);
            }
        }

        public void dock()
        {
            dock(defaultDockStyle);
        }
        public void dock(DockStyle dockStyle)
        {
            if (!viewable)
            {
                Dock = dockStyle;
                parent.Controls.Add(this);
                viewable = true;
                fireDockedEvt(new DockControlDockChangedEventArgs(this));

                resizeStrip.Visible = true;
                switch (dockStyle)
                {
                    case DockStyle.Bottom:
                        resizeStrip.Dock = DockStyle.Top;
                        break;
                    case DockStyle.Left:
                        resizeStrip.Dock = DockStyle.Right;
                        break;
                    case DockStyle.Right:
                        resizeStrip.Dock = DockStyle.Left;
                        break;
                    case DockStyle.Top:
                        resizeStrip.Dock = DockStyle.Bottom;
                        break;
                    default:
                        resizeStrip.Dock = DockStyle.None;
                        break;
                }
            }
        }

        public void undock()
        {
            if (viewable)
            {
                parent.Controls.Remove(this);
                viewable = false;
                fireUndockedEvt(new DockControlDockChangedEventArgs(this));
            }
        }


        private void configureControls()
        {

            //close button control
            closeBtn.Click += (s, args) => {
                undock();
            };

            resizeStrip.MouseMove += (s, a) =>
            {
                if (resizeStrip.Dock == DockStyle.Top || resizeStrip.Dock == DockStyle.Bottom)
                    Cursor.Current = Cursors.HSplit;
                else Cursor.Current = Cursors.VSplit;
            };
            resizeStrip.MouseLeave += (s, a) =>
            {
                Cursor.Current = Cursors.Default;
            };

            //resize control
            resizeStrip.MouseDown += (s, a) =>
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.HSplit;
                
                Boolean abort = true;
                Thread resizeThread = new Thread(() =>
                {

                    resizeStrip.MouseUp += (s2, a2) =>
                    {
                        abort = false;
                    };
                   
                    while (abort)
                    {
                        try
                        {
                            if (resizeStrip.Dock == DockStyle.Top)
                            {
                                Invoke(new NoArgFunction(() =>
                                {
                                    int yDelta = resizeStrip.PointToScreen(resizeStrip.Location).Y - Cursor.Position.Y;
                                    Size = new Size(Width, Height + yDelta);
                                }));
                            }
                            else if (resizeStrip.Dock == DockStyle.Right)
                            {
                                Invoke(new NoArgFunction(() =>
                                {
                                    int xDelta = Cursor.Position.X - resizeStrip.PointToScreen(resizeStrip.Location).X + Width - resizeStrip.Width;
                                    Size = new Size(Width + xDelta, Height);
                                }));
                            }
                            else if (resizeStrip.Dock == DockStyle.Left)
                            {
                                Invoke(new NoArgFunction(() =>
                                {
                                    int xDelta = Cursor.Position.X - resizeStrip.PointToScreen(resizeStrip.Location).X;// +Width - resizeStrip.Width;
                                    Console.WriteLine(xDelta);
                                     Size = new Size(Width - xDelta, Height);
                                }));
                            }
                            else if (resizeStrip.Dock == DockStyle.Bottom)
                            {
                                throw new NotImplementedException("HELP");
                            }

                            Thread.Sleep(1);
                        }
                        catch (ThreadAbortException e)
                        {
                            
                        }


                    }

                    Invalidate();
                });


                resizeThread.Start();

            };

         

        }



        private void initControls()
        {
            this.Padding = new Padding(0);
            this.Margin = new Padding(0);

            resizeStrip = new StatusStrip();
            resizeStrip.Size = new Size(4, 4);
            resizeStrip.GripStyle = ToolStripGripStyle.Hidden;
            resizeStrip.SizingGrip = true;
            resizeStrip.AutoSize = false;
            resizeStrip.Dock = DockStyle.Top;
            resizeStrip.BackColor = Color.Black;

            menuPanel = new Panel();
            menuPanel.BackColor = UIManager.Instance.FlairColor ;
            menuPanel.Dock = DockStyle.Fill;
            menuPanel.Size = new Size(40, 20);
            menuPanel.Padding = new Padding(0);
            menuPanel.Margin = new Padding(1);
            menuPanel.BorderStyle = BorderStyle.None;

            closeBtn = new Button();
            closeBtn.Text = "X";
            closeBtn.FlatStyle = FlatStyle.Flat;
            closeBtn.FlatAppearance.BorderSize = 0;
            closeBtn.Size = new Size(16, 16);
            closeBtn.Dock = DockStyle.Right;
            menuPanel.Controls.Add(closeBtn);

            label = new Label();
            label.Text = "SomeText";
            label.Location = new Point(2, 2);
            menuPanel.Controls.Add(label);

            tablePanel = new TableLayoutPanel();
            tablePanel.Size = new Size(50, 50);
            tablePanel.BackColor = UIManager.Instance.DullFlairColor;
            tablePanel.RowCount = 2;
            tablePanel.ColumnCount = 1;
            tablePanel.Padding = new Padding(0);
            tablePanel.Margin = new Padding(0);
            tablePanel.Controls.Add(menuPanel, 0, 0);
        }

        private void addControls()
        {
            tablePanel.Dock = DockStyle.Fill;
            Controls.Add(resizeStrip);
            Controls.Add(tablePanel);
        }

    }
}
