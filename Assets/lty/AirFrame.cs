using DeveloperKit.Runtime.GameFramework;

namespace lty
{
    public class AirFrame : GameFramework<AirFrame>
    {
        public override void Init()
        {
        }

        public override IFramework GetFramework()
        {
            return AirFrame.Interface;
        }
    }
}


