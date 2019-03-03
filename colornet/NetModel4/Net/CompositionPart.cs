using System.Collections.Generic;

namespace Net
{

    /// <summary>
    /// Part of composition. Define number of pixels of an image.
    /// </summary>
    public class CompositionPart
    {
        /// <summary>
        /// Parent composition area
        /// </summary>
        CompositionPart parent;

        /// <summary>
        /// Composed parts should be null if canvasArea is not null.
        /// </summary>
        HashSet<CompositionPart> parts;

        /// <summary>
        /// Canvas area should be null if parts is not null.
        /// </summary>
        CanvasArea canvasArea;

        public CompositionPart(CompositionPart parent, IEnumerable<CompositionPart> parts)
        {
            this.parent = parent;
            this.parts = new HashSet<CompositionPart>(parts);
            this.canvasArea = null;
        }

        public CompositionPart(CompositionPart parent, CanvasArea canvasArea)
        {
            this.parent = parent;
            this.canvasArea = canvasArea;
            this.parts = null;
        }

        /// <summary>
        /// Get combined area of the parts
        /// </summary>
        /// <returns>Canvas Area for a combined area of the parts</returns>
        public CanvasArea UnionParts()
        {
            var canvasArea = CanvasArea.Empty;

            if (parts!= null)
            {
                foreach(var part in parts)
                {
                    canvasArea = canvasArea.Union(part.UnionParts());
                }
            }

            return canvasArea;
        }

    }
}