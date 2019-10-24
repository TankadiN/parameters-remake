using System;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
    public static class UIUtilities
    {
        public static string ValueToDisplayString(int value)
        {
            if(value < 1000) // 0 - 999
            {
                return value.ToString();
            }
            else if(value < 100000) // 1.0K - 99.9K
            {
                // remove tens
                float truncatedValue = (value / 100) / 10f;
                return(truncatedValue.ToString() + "K");
            }
            else if(value < 10000000) // 100K - 999K
            {
                // remove hundreds
                int truncatedValue = (value / 1000);
                return(truncatedValue.ToString() + "K");
            }
            else if(value < 1000000000) // 1.0M - 99.9M
            {
                // remove tens of thousands
                float truncatedValue = (value / 100000) / 10f;
                return(truncatedValue.ToString() + "M");
            }
            else // 100M+
            {
                // remove hundreds of thousands
                int truncatedValue = (value / 1000000);
                return(truncatedValue.ToString() + "M");
            }
        }

        // TODO(@jackson): Add smallest unit param
        public static string ByteCountToDisplayString(Int64 value)
        {
            string[] sizeSuffixes = new string[]{"B", "KB", "MB", "GB"};
            int sizeIndex = 0;
            Int64 adjustedSize = value;
            Int64 lastSize = 0;
            while(adjustedSize > 0x0400
                  && (sizeIndex+1) < sizeSuffixes.Length)
            {
                lastSize = adjustedSize;
                adjustedSize /= 0x0400;
                ++sizeIndex;
            }

            if(sizeIndex > 0
               && adjustedSize < 100)
            {
                decimal displayValue = (decimal)lastSize / (decimal)0x0400;
                return displayValue.ToString("0.0") + sizeSuffixes[sizeIndex];
            }
            else
            {
                return adjustedSize + sizeSuffixes[sizeIndex];
            }
        }

        public static Sprite CreateSpriteFromTexture(Texture2D texture)
        {
            return Sprite.Create(texture,
                                 new Rect(0.0f, 0.0f, texture.width, texture.height),
                                 Vector2.zero);
        }

        public static void OpenYouTubeVideoURL(string youTubeVideoId)
        {
            if(!String.IsNullOrEmpty(youTubeVideoId))
            {
                Application.OpenURL(@"https://youtu.be/" + youTubeVideoId);
            }
        }

        /// <summary>Counts the cells that will fit in within the RectTransform of the given grid</summary>
        public static int CountVisibleGridCells(GridLayoutGroup gridLayout)
        {
            Debug.Assert(gridLayout != null);

            // calculate dimensions
            RectTransform transform = gridLayout.GetComponent<RectTransform>();
            Vector2 gridDisplayDimensions = new Vector2();
            gridDisplayDimensions.x = (transform.rect.width
                                       - gridLayout.padding.left
                                       - gridLayout.padding.right
                                       + gridLayout.spacing.x);
            gridDisplayDimensions.y = (transform.rect.height
                                       - gridLayout.padding.top
                                       - gridLayout.padding.bottom
                                       + gridLayout.spacing.y);

            // calculate cell count
            int columnCount = 0;
            if(gridLayout.cellSize.x + gridLayout.spacing.x > 0f)
            {
                columnCount = (int)Mathf.Floor(gridDisplayDimensions.x
                                               / (gridLayout.cellSize.x + gridLayout.spacing.x));

            }
            int rowCount = 0;
            if((gridLayout.cellSize.y + gridLayout.spacing.y) > 0f)
            {
                rowCount = (int)Mathf.Floor(gridDisplayDimensions.y
                                            / (gridLayout.cellSize.y + gridLayout.spacing.y));
            }

            // check constraints
            if(gridLayout.constraint == GridLayoutGroup.Constraint.FixedColumnCount)
            {
                if(gridLayout.constraintCount < columnCount)
                {
                    columnCount = gridLayout.constraintCount;
                }
            }
            else if(gridLayout.constraint == GridLayoutGroup.Constraint.FixedRowCount)
            {
                if(gridLayout.constraintCount < rowCount)
                {
                    rowCount = gridLayout.constraintCount;
                }
            }

            return rowCount * columnCount;
        }
    }
}
