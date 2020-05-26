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
    class circleClass : baseClass
    {
        private int speed = 8;
        int level;

        public circleClass(Game game, ref Texture2D _sprTexture, Vector2 _sprPosition, Rectangle _sprRectangle, int level)
        : base(game, ref _sprTexture, _sprPosition, _sprRectangle)
        {
            sprTexture = _sprTexture;
            sprPosition = _sprPosition;
            sprRectangle = _sprRectangle;
            this.level = level;
        }

        public void Move()
        {
            if (level == 1)
            {
                sprPosition.X += speed;
            }
            else
            {
                sprPosition.X = 250;
                sprPosition.Y +=speed;
            }
        }

        public override void Initialize()
        {

            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {
            Move();
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
