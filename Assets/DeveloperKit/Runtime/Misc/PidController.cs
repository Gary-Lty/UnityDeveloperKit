using UnityEngine;

[System.Serializable]
public class PidController
{
    /// <summary>
    /// 比例增益系数
    /// </summary>
    public float proportionalGain = 0;

    /// <summary>
    /// 积分增益系数
    /// </summary>
    public float integralGain = 0;

    /// <summary>
    /// 微分增益系数
    /// </summary>
    public float derivativeGain = 0;
    
    /// <summary>
    /// 积分最大值
    /// </summary>
    public float integralSaturation;


    private float errLast;
    private float valueLast;
    private float p;
    private float d;
    private float i;
    private float integralStored;

    public float P => p;
    public float D => d;

    public float I => i;

    public DerivativeMeasurement derivativeMeasurement;
    private bool derivativeInit;

    public enum DerivativeMeasurement
    {
        //输入值的改变率
        ValueRate,

        //差值的改变率
        ErrorRate,
    }

    /// <summary>
    /// 计算运动必须在fixed update
    /// </summary>
    /// <param name="currentValue"></param>
    /// <param name="targetValue"></param>
    /// <param name="dt"></param>
    /// <returns></returns>
    public float FixedUpdate(float currentValue, float targetValue, float dt = 0.02f)
    {
        var err = targetValue - currentValue;
        p = err * proportionalGain;

        if (!derivativeInit)
        {
            valueLast = currentValue;
            errLast = err;
            derivativeInit = true;
        }
        else
        {
            if (derivativeMeasurement == DerivativeMeasurement.ErrorRate)
            {
                var errRateOfChange = (err - errLast) / dt;
                errLast = err;
                d = errRateOfChange * derivativeGain;
            }
            else
            {
                var valueRateOfChange = (valueLast - currentValue) / dt;
                valueLast = currentValue;
                d = valueRateOfChange * derivativeGain;
            }
        }

        integralStored = integralStored + (err * dt);
        integralStored = Mathf.Clamp(integralStored, -integralSaturation, integralSaturation);
        i = integralGain * integralStored;
        return p + d + i;
    }

    /// <summary>
    /// 求角度的pid
    /// </summary>
    /// <param name="currentAngle"></param>
    /// <param name="targetAngle"></param>
    /// <param name="dt"></param>
    /// <returns></returns>
    public float UpdateAngle(float currentAngle, float targetAngle, float dt)
    {
        var err = AngleDifference(targetAngle, currentAngle);
        p = err * proportionalGain;

        if (!derivativeInit)
        {
            valueLast = targetAngle;
            errLast = err;
            derivativeInit = true;
        }
        else
        {
            if (derivativeMeasurement == DerivativeMeasurement.ErrorRate)
            {
                var errRateOfChange = AngleDifference(err,errLast)/ dt;
                errLast = err;
                d = errRateOfChange * derivativeGain;
            }
            else
            {
                var valueRateOfChange = AngleDifference(valueLast,err) / dt;
                valueLast = currentAngle;
                d = valueRateOfChange * derivativeGain;
            }
        }

        integralStored = integralStored + (err * dt);
        integralStored = Mathf.Clamp(integralStored, -integralSaturation, integralSaturation);
        i = integralGain * integralStored;
        return p + d + i;
    }

    /// <summary>
    /// 在传送，pid长时间不用后重置
    /// </summary>
    public void Reset()
    {
        derivativeInit = false;
        integralStored = 0;
        valueLast = 0;
        errLast = 0;
    }


    /// <summary>
    /// 计算ab角之间的最小夹角，返回值在-180~180
    /// </summary>
    /// <param name="angleA"></param>
    /// <param name="angleB"></param>
    /// <returns></returns>
    public float AngleDifference(float angleA, float angleB)
    {
        return (angleA - angleB + 540) % 360 - 180;
    }
}