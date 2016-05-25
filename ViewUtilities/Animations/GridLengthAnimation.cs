//-----------------------------------------------------------------------
// <copyright file="GridLengthAnimation.cs" company="Owlet Care Inc">
//     Copyright (c) Owlet Care Inc.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ViewUtilities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Media.Animation;

    /// <summary>
    /// GridLengthAnimation class implementation.
    /// </summary>
    public class GridLengthAnimation : AnimationTimeline
    {
        #region Fields

        /// <summary>
        /// The From dependency property.
        /// </summary>
        public static readonly DependencyProperty FromProperty = DependencyProperty.Register(
            "From",
            typeof(GridLength),
            typeof(GridLengthAnimation));

        /// <summary>
        /// The To dependency property.
        /// </summary>
        public static readonly DependencyProperty ToProperty = DependencyProperty.Register(
            "To",
            typeof(GridLength),
            typeof(GridLengthAnimation));

        #endregion

        #region Events

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Type of property that can be animated.
        /// </summary>
        public override Type TargetPropertyType
        {
            get
            {
                return typeof(GridLength);
            }
        }

        /// <summary>
        /// Gets or sets the From property.
        /// </summary>
        public GridLength From
        {
            get
            {
                return (GridLength)this.GetValue(FromProperty);
            }

            set
            {
                this.SetValue(FromProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the To property.
        /// </summary>
        public GridLength To
        {
            get
            {
                return (GridLength)this.GetValue(ToProperty);
            }

            set
            {
                this.SetValue(ToProperty, value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the current value of the animation.
        /// </summary>
        /// <param name="defaultOriginValue">
        /// The origin value provided to the animation if the animation does not have its own start value.
        /// </param>
        /// <param name="defaultDestinationValue">
        /// The destination value provided to the animation if the animation does not have its own destination value.
        /// </param>
        /// <param name="animationClock">
        /// The AnimationClock which can generate the CurrentTime or CurrentProgress value
        /// to be used by the animation to generate its output value.
        /// </param>
        /// <returns>The value this animation believes should be the current value for the property.</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods",
            Justification = "Cannot validate an AnimcationClock")]
        public override object GetCurrentValue(
            object defaultOriginValue,
            object defaultDestinationValue,
            AnimationClock animationClock)
        {
            var from = this.From == default(GridLength) ? (GridLength)defaultOriginValue : this.From;
            var to = this.To == default(GridLength) ? (GridLength)defaultDestinationValue : this.To;
            var fromValue = from.Value;
            var toValue = to.Value;

            if (null == animationClock.CurrentProgress)
            {
                return this.From;
            }

            if (fromValue > toValue)
            {
                return new GridLength(
                    ((1 - animationClock.CurrentProgress.Value) * (fromValue - toValue)) + toValue,
                    this.To.GridUnitType);
            }

            return new GridLength(
                (animationClock.CurrentProgress.Value * (toValue - fromValue)) + fromValue,
                this.To.GridUnitType);
        }

        /// <summary>
        /// Creates a new instance of the Freezable derived class.
        /// </summary>
        /// <returns>The new instance.</returns>
        protected override Freezable CreateInstanceCore()
        {
            return new GridLengthAnimation();
        }

        #endregion
    }
}
