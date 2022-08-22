using UnityEngine;

public static class MathExtension {

   public static Vector3 GetDirectionFromAngle(float angle, Vector3 eulerAngles) {

        angle += eulerAngles.y;
        return GetDirectionFromGlobalAngle(angle);

    }

    public static Vector3 GetDirectionFromGlobalAngle(float angle) {

        float x = Mathf.Sin(angle * Mathf.Deg2Rad);
        float y = Mathf.Cos(angle * Mathf.Deg2Rad);
        float z = 0;

        Vector3 direction = new Vector3(x, y, z);
        return direction;

    }

    public static bool IsInRangeClosedInterval(float testValue, float minBound, float maxBound) {

        float realMaxBound = Mathf.Max(minBound, maxBound);
        float realMinBound = Mathf.Min(minBound, maxBound);

        bool isLowThanMax = testValue <= realMaxBound;
        bool isHigherThanMin = testValue >= realMinBound;

        return (isHigherThanMin && isLowThanMax);

    }

    public static bool IsInRangeOpenInterval(float testValue, float minBound, float maxBound) {

        float realMaxBound = Mathf.Max(minBound, maxBound);
        float realMinBound = Mathf.Min(minBound, maxBound);

        bool isLowThanMax = testValue < realMaxBound;
        bool isHigherThanMin = testValue > realMinBound;

        return (isHigherThanMin && isLowThanMax);

    }

}
