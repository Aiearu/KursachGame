using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace lwar
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        float timer = 1;
        float timer1=1;
        float timer2 = 0;
        int i = 0;
        SpriteFont font;

        int level = 1;
        int score = 0;
        bool win = false;
        bool lose = false;
        Random randNum = new Random();
        int count = 0;

        Texture2D textureCircle, textureSquare;
        Texture2D backLvl1, backLvl2, defeatFon,winFon;
        MouseState mouse;
        KeyboardState kbstate;
        Song song;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 540;
            graphics.PreferredBackBufferHeight = 460;
            graphics.ApplyChanges();

            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);

            textureCircle = Content.Load<Texture2D>("circle");
            textureSquare = Content.Load<Texture2D>("square");
            backLvl1 = Content.Load<Texture2D>("backLvl1");
            backLvl2 = Content.Load<Texture2D>("backLvl2");
            defeatFon = Content.Load<Texture2D>("defeat");
            winFon = Content.Load<Texture2D>("win");
            font = Content.Load<SpriteFont>("File");
            song = Content.Load<Song>("Deep_Purple_Clearly_Quite_Absurd");

            MediaPlayer.Play(song);
            // повторять после завершения
            MediaPlayer.IsRepeating = true;
            // прикрепляем обработчик изменения состояния проигрывания мелодии
        }
        void CreateSquares()
        {
            if (timer >= 2 )         
            {

                i++;
                Components.Add(new squareClass(this, ref textureSquare, new Vector2(0, 0), new Vector2(500, 500), i, new Rectangle(0, 0, 64, 64), level));
                count++;
                timer = 0;
            }
        }

        bool IsCollideWithObject(baseClass spr, baseClass spr1)
        {
            return (spr1.sprPosition.X + spr1.sprRectangle.Width > spr.sprPosition.X &&
                        spr1.sprPosition.X < spr.sprPosition.X + spr.sprRectangle.Width &&
                        spr1.sprPosition.Y + spr1.sprRectangle.Height > spr.sprPosition.Y &&
                        spr1.sprPosition.Y < spr.sprPosition.Y + spr.sprRectangle.Height);

        }
        void IsCollideWithAny()
        {

            bool contact = false;
            baseClass sq = null;
            baseClass circle = null;
            foreach (baseClass spr1 in Components)
            {
                if (spr1.GetType() == (typeof(circleClass)))
                {
                    foreach (baseClass spr in Components)
                    {

                        if (spr.GetType() == (typeof(squareClass)))
                        {
                            if (IsCollideWithObject(spr1, spr))
                            {
                                circle = spr1;
                                sq = spr;
                                contact = true;
                            }
                        }
                    }
                }
            }
            if (contact)
            {
                Components.Remove(sq);
                //count++;
                score++;
                contact = false;
            }
        }
        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (score >= 10)
                level = 2;
            if (score < 20 && !lose)
            {
                kbstate = Keyboard.GetState();
                mouse = Mouse.GetState();
                if (count < 20 )
                {
                    if (level == 1 && count < 10)
                    {
                        CreateSquares();
                    }
                    else if (level == 2)
                    {
                        CreateSquares();
                    }
                }
                IsCollideWithAny();
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                timer1 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                timer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (kbstate.IsKeyDown(Keys.Space) && timer1 >= 1)
                {
                    if (level == 1)
                    {
                        Components.Add(new circleClass(this, ref textureCircle, new Vector2(0, 200), new Rectangle(0, 0, 64, 64), level));
                    }
                    else
                        Components.Add(new circleClass(this, ref textureCircle, new Vector2(0, 0), new Rectangle(0, 0, 64, 64), level));
                    timer1 = 0;
                }
            }
            else
            {
                win = true;
            }
            if (timer2 >= 70)
            {
                lose = true;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            Color colorText;
            if (!lose)
            {
                if (level == 1)
                {
                    colorText = Color.Black;
                    spriteBatch.Draw(backLvl1, Vector2.Zero);
                }
                else
                {
                    colorText = Color.White;
                    spriteBatch.Draw(backLvl2, new Rectangle(0, 0, 564, 564), Color.White);
                }
                if (!win)
                {
                    spriteBatch.DrawString(font, count.ToString() + "Счет: " + score.ToString() + " Уровень " + level.ToString() + " Времени прошло: " + ((int)timer2).ToString() + " секунд", new Vector2(0, 0), colorText);
                }
                else
                {
                    spriteBatch.Draw(winFon, new Rectangle(0, 0, 564, 564), Color.White);
                    spriteBatch.DrawString(font, "Счет: " + score.ToString() + " Вы выиграли за " + ((int)timer2).ToString() + " секунд", new Vector2(0, 0), Color.Black);
                }

                base.Draw(gameTime);
            }
            else
            {
                spriteBatch.Draw(defeatFon, new Rectangle(0, 0, 564, 564), Color.White);
                spriteBatch.DrawString(font, "Счет: " + score.ToString() + " Вы поиграли - вы потратили \n слишком много времени", new Vector2(0, 0), Color.Red);

            }
            spriteBatch.End();
        }
    }
}
