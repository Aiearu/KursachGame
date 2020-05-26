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
    class baseClass : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public Texture2D sprTexture;
        public Vector2 sprPosition;
        public Rectangle sprRectangle;

        public baseClass(Game game, ref Texture2D _sprTexture,
            Vector2 _sprPosition, Rectangle _sprRectangle)
            : base(game)
        {
            sprTexture = _sprTexture;
            sprPosition = _sprPosition;
            sprRectangle = _sprRectangle;
        }


        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
