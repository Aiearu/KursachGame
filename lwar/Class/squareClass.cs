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

namespace lwar
{
    class squareClass : baseClass
    {
        protected Random randNum = new Random();
        int speed;
        bool up = true;
        private Vector2 finishPosition;
        private Vector2 startPosition;
        bool Diagonal;
        int level;


        public squareClass(Game game, ref Texture2D _sprTexture, Vector2 _sprPosition,Vector2 finish, int _speed, Rectangle _sprRectangle, int level)
        : base(game, ref _sprTexture, _sprPosition, _sprRectangle)
        {
            sprTexture = _sprTexture;
            sprPosition = _sprPosition;
            sprRectangle = _sprRectangle;
            speed = _speed;
            finishPosition = finish;
            startPosition = _sprPosition;
            this.level = level;
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        void MoveD()
        {
            if (level == 1)
            {
                if (startPosition.Y == 0)
                {
                    Diagonal = false;
                }
                else
                    Diagonal = true;

                if (up)
                {
                    if (sprPosition.X < finishPosition.X)
                    {
                        if (Diagonal)
                        {
                            sprPosition.X += speed;
                            sprPosition.Y -= speed;
                        }
                        else
                        {
                            sprPosition.X += speed;
                            sprPosition.Y += speed;
                        }
                    }
                    else
                    {
                        sprPosition.X = finishPosition.X;
                        sprPosition.Y = finishPosition.Y;
                        up = false;
                    }
                }
                else
                {
                    if (sprPosition.X > startPosition.X)
                    {
                        if (Diagonal)
                        {
                            sprPosition.X -= speed;
                            sprPosition.Y += speed;
                        }
                        else
                        {
                            sprPosition.X -= speed;
                            sprPosition.Y -= speed;
                        }
                    }
                    else
                    {
                        sprPosition.X = startPosition.X;
                        sprPosition.Y = startPosition.Y;
                        up = true;
                    }
                }
            }
            else
            {

                /*if (sprPosition.X != finishPosition.X)
                {
                    if(sprPosition.)
                    sprPosition.X += speed;
                }
                else
                {
                    sprPosition.X = finishPosition.X;
                    float temp = sprPosition.X;
                    finishPosition.X = startPosition.X;
                    startPosition.X =temp;
                    speed *= -1;
                }

                sprPosition.Y = 250;

            }*/
                sprPosition.Y = 250;
                if (Vector2.Distance(new Vector2(finishPosition.X, 250), new Vector2(sprPosition.X, 250)) < speed)
                {
                    finishPosition.X = startPosition.X;
                    startPosition.X = sprPosition.X;
                }
                else
                {
                    Vector2 direction = new Vector2(finishPosition.X, 250) - new Vector2(sprPosition.X, 250);
                    direction.Normalize();//вектор того же направления но единичной длины
                    Vector2 velocity = Vector2.Multiply(direction, speed);
                    sprPosition += velocity;
                }
            }
        }


        public override void Update(GameTime gameTime)
        {
            MoveD();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sprBatch =
                (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            sprBatch.Draw(sprTexture, sprPosition, Color.White);
            base.Draw(gameTime);
        }
    }
}
