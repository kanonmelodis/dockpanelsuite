using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WeifenLuo.WinFormsUI.Docking
{
    /// <summary>
    /// Dock window of Visual Studio 2012 Light theme.
    /// </summary>
    [ToolboxItem(false)]
    internal class VS2012LightDockWindow : DockWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VS2012LightDockWindow"/> class.
        /// </summary>
        /// <param name="dockPanel">The dock panel.</param>
        /// <param name="dockState">State of the dock.</param>
        public VS2012LightDockWindow(DockPanel dockPanel, DockState dockState) : base(dockPanel, dockState)
        {
        }

        public override Rectangle DisplayingRectangle
        {
            get
            {
                Rectangle rect = ClientRectangle;
                if (DockState == DockState.DockLeft)
                    rect.Width -= Measures.SplitterSize;
                else if (DockState == DockState.DockRight)
                {
                    rect.X += Measures.SplitterSize;
                    rect.Width -= Measures.SplitterSize;
                }
                else if (DockState == DockState.DockTop)
                    rect.Height -= Measures.SplitterSize;
                else if (DockState == DockState.DockBottom)
                {
                    rect.Y += Measures.SplitterSize;
                    rect.Height -= Measures.SplitterSize;
                }

                return rect;
            }
        }
        
        internal class VS2012LightDockWindowSplitterControl : SplitterBase
        {
            private static readonly SolidBrush _horizontalBrush = new SolidBrush(Color.FromArgb(0xFF, 204, 206, 219));
			private static readonly Color[] _verticalSurroundColors = new[] { Color.FromArgb(255, 245, 245, 245) };

            protected override int SplitterSize
            {
                get { return Measures.SplitterSize; }
            }

            protected override void StartDrag()
            {
                DockWindow window = Parent as DockWindow;
                if (window == null)
                    return;

                window.DockPanel.BeginDrag(window, window.RectangleToScreen(Bounds));
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                Rectangle rect = ClientRectangle;

                if (rect.Width <= 0 || rect.Height <= 0)
                    return;

                switch (Dock)
                {
                    case DockStyle.Right:
                    case DockStyle.Left:
                        {
                            using (var path = new GraphicsPath())
                            {
                                path.AddRectangle(rect);
                                using (var brush = new PathGradientBrush(path)
                                    {
                                        CenterColor = Color.FromArgb(0xFF, 204, 206, 219), SurroundColors = _verticalSurroundColors
                                    })
                                {
                                    e.Graphics.FillRectangle(brush, rect.X + Measures.SplitterSize / 2 - 1, rect.Y, 
                                                             Measures.SplitterSize/3, rect.Height);
                                }
                            }
                        }
                        break;
                    case DockStyle.Bottom:
                    case DockStyle.Top:
                        {
                            e.Graphics.FillRectangle(_horizontalBrush, rect.X, rect.Y,
                                                     rect.Width, Measures.SplitterSize);
                        }
                        break;
                }

            }
        }

		internal class VS2012DarkDockWindowSplitterControl : SplitterBase {
			private static readonly SolidBrush _horizontalBrush = new SolidBrush( Color.FromArgb( 0xFF, 37, 37, 38 ) );
			private static readonly Color _horizontalColor = Color.FromArgb( 255, 37, 37, 38 );
			private static readonly Color[] _verticalSurroundColors = new[] { Color.FromArgb( 255, 45, 45, 48 ) };

			protected override int SplitterSize {
				get { return Measures.SplitterSize; }
			}

			protected override void StartDrag() {
				DockWindow window = Parent as DockWindow;
				if ( window == null )
					return;

				window.DockPanel.BeginDrag( window, window.RectangleToScreen( Bounds ) );
			}

			protected override void OnPaint( PaintEventArgs e ) {
				base.OnPaint( e );

				Rectangle rect = ClientRectangle;

				if ( rect.Width <= 0 || rect.Height <= 0 )
					return;

				switch ( Dock ) {
					case DockStyle.Right:
					case DockStyle.Left: {
							using ( var path = new GraphicsPath() ) {
								path.AddRectangle( rect );
								using ( var brush = new PathGradientBrush( path ) {
									CenterColor = _horizontalColor,
									SurroundColors = _verticalSurroundColors
								} ) {
									e.Graphics.FillRectangle( brush, rect.X + Measures.SplitterSize / 2 - 1, rect.Y,
															 Measures.SplitterSize / 3, rect.Height );
								}
							}
						}
						break;
					case DockStyle.Bottom:
					case DockStyle.Top: {
							e.Graphics.FillRectangle( _horizontalBrush, rect.X, rect.Y,
													 rect.Width, Measures.SplitterSize );
						}
						break;
				}

			}
		}
    }
}
