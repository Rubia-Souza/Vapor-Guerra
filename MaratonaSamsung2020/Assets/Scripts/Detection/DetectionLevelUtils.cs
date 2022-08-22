public static class DetectionLevelUtils {

    public const float UnseenDetectionPorcentage = 0f;
    public const float LowDetectionPorcentage = 0.2f;
    public const float MediumDetectionPorcentage = 0.5f;
    public const float HighDetectionPorcentage = 0.7f;
    public const float DetectedDetectionPorcentage = 1f;

    public static DetectionLevels GetDetectionLevel(float detectionLevelPorcentage) {

        float detectionPorcentage = detectionLevelPorcentage;

        if (detectionPorcentage >= UnseenDetectionPorcentage && detectionPorcentage < LowDetectionPorcentage) {

            return DetectionLevels.UNSEEN;

        } 
        else if (detectionPorcentage >= LowDetectionPorcentage && detectionPorcentage < MediumDetectionPorcentage) {

            return DetectionLevels.LOW;

        } 
        else if (detectionPorcentage >= MediumDetectionPorcentage && detectionPorcentage < HighDetectionPorcentage) {

            return DetectionLevels.MEDIUM;

        } 
        else if (detectionPorcentage >= HighDetectionPorcentage && detectionPorcentage < DetectedDetectionPorcentage) {

            return DetectionLevels.HIGH;

        } 
        else {

            return DetectionLevels.DETECTED;

        }

    }

    public static float GetDetectionLevelPorcentage(DetectionLevels level) {

        float detectionPorcentage;

        switch (level) {

            case DetectionLevels.LOW:

                detectionPorcentage = LowDetectionPorcentage;

                break;

            case DetectionLevels.MEDIUM:

                detectionPorcentage = MediumDetectionPorcentage;

                break;

            case DetectionLevels.HIGH:

                detectionPorcentage = HighDetectionPorcentage;

                break;

            case DetectionLevels.DETECTED:

                detectionPorcentage = DetectedDetectionPorcentage;

                break;

            default:

                detectionPorcentage = UnseenDetectionPorcentage;

                break;

        }

        return detectionPorcentage;

    }
    
}
