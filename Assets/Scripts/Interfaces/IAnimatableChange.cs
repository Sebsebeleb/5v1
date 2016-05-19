// This interface should be used every change in game state that CAN (they may say they should not be animated) be animatable. The interface's object is to describe how it is animated.

namespace BBG.Interfaces
{
    using BBG.View;

    public interface IAnimatableChange{
        bool ShouldAnimate();
        ChangeAnimation GetAnimationInfo();	
    }
}