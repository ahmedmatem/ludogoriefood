namespace LudogorieFood.Web.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Models;

    public class LinkedSlideList
    {
        public List<Slide> Slides { get; set; }

        public LinkedSlideList(List<Slide> slides)
        {

            this.Slides = new List<Slide>();

            var firstSlide = this.GetFirstSlide(slides);
            if(firstSlide == null)
            {
                this.Slides = new List<Slide>();
                return;
            }

            this.Slides.Add(firstSlide);
            var currentSlide = firstSlide;
            while(currentSlide != null && currentSlide.NextSlideId != null)
            {
                var nextSlide = this.FindSlideAfter(slides, currentSlide);
                this.Slides.Add(nextSlide);
                currentSlide = nextSlide;
            }
        }

        private Slide GetFirstSlide(List<Slide> slides)
        {
            foreach (var slide in slides)
            {
                if(slide.PrevSlideId == null)
                {
                    return slide;
                }
            }

            return null;
        }

        private Slide FindSlideAfter(List<Slide> slides, Slide slide)
        {
            if(slide.NextSlideId == null)
            {
                return null;
            }

            foreach (var sld in slides)
            {
                if (sld.NextSlideId == slide.Id)
                {
                    return slide;
                }
            }

            return null;
        }
        
    }
}
