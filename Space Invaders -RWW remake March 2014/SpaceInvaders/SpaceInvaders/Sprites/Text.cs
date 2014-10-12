using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Sprites
{
    class Text
    {
        private TextureObj texture;
        private String asset;
        private TextKey key;
        private float sz;
        private int height;
        private int width;
        private FormatType format;
        private bool alpha;
        private TextType type;

        public Text(TextureObj text, String AssetName, TextKey key, float byteSize, FormatType Format, bool alpha, TextType type)
        {
            texture = text;
            asset = AssetName;
            this.key = key;
            sz = byteSize;
            format = Format;
            this.alpha = alpha;
            this.type = type;
            if (type == TextType.SF) height = width = 0;
            if (type == TextType.T2D)
            {
                height = ((Text2D)text).Instance.Bounds.Height;
                width = ((Text2D)text).Instance.Bounds.Width;
            }
        }

        public TextureObj getTexture()
        {
            return texture;
        }

        public String AssetName()
        {
            return asset;
        }

        public TextKey textkey()
        {
            return key;
        }

        public float size()
        {
            return sz;
        }

        public Vector2 dimensions()
        {
            return new Vector2(width, height);
        }

        public FormatType getFormat()
        {
            return format;
        }

        public bool hasAlpha()
        {
            return alpha;
        }

        public TextType getType()
        {
            return type;
        }
    }
}
