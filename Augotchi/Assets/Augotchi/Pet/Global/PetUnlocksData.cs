using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PetUnlocksData {

    public enum HatType {
        BEANIE,
        PARTYHAT,
        TOPHAT,
        BERET,
        TIARA,
        TUPE,
        COPTERHAT,
        CROWN,
        BOWTIE,
        FILTKEPS
    }

    // Hat Numbers

    public readonly static int[] hatCounts = new int[15]
    {
        1,
        6,
        4,
        3,
        6,
        5,
        4,
        4,
        4,
        7,
        6,
        6,
        4,
        4,
        1
    };

    // HAT Unlock Bitmasks

    public int[] unlockedHats = new int[15];

    // Faces Unlock Bitmasks

    public readonly static int[] faceCounts = new int[13]
{
        1,
        5,
        4,
        5,
        4,
        1,
        5,
        6,
        6,
        3,
        3,
        4,
        3
};

    public int[] unlockedFaces = new int[13];


    public bool isHatUnlocked(int hatIndex, int hatVariation)
    {
        if(unlockedHats == null)
            unlockedHats = new int[14];

        return (unlockedHats[hatIndex] & 1 << hatVariation) > 0;
    }

    public bool unlockHat(int hatIndex, int hatVariation)
    {
        bool isUnlocked = isHatUnlocked(hatIndex, hatVariation);
        if (isUnlocked)
        {
            return false;
        }

        unlockedHats[hatIndex] = unlockedHats[hatIndex] | (1 << hatVariation);
        return true;
    }

    public bool hatCategoryHasUnlocks(int hatIndex)
    {
        return unlockedHats[hatIndex] > 0;
    }

    public int currentEquippedHatAbsoluteIndex(int hatIndex)
    {
        if (hatIndex == 0)
            return 0;

        int relativeIndex = 0;
        int absoluteIndex = 0;

        for (int i = 1; i < unlockedHats.Length; i++)
        {
            absoluteIndex++;
            if (unlockedHats[i] > 0)
                relativeIndex++;

            if (hatIndex == relativeIndex)
                return absoluteIndex;
        }

        return 100;
    }

    public bool isFaceUnlocked(int faceIndex, int faceVariation)
    {
        if (unlockedFaces == null)
            unlockedFaces = new int[13];
        return (unlockedFaces[faceIndex] & 1 << faceVariation) > 0;
    }

    public bool unlockFace(int faceIndex, int faceVariation)
    {
        bool isUnlocked = isFaceUnlocked(faceIndex, faceVariation);
        if (isUnlocked)
        {
            return false;
        }

        unlockedFaces[faceIndex] = unlockedFaces[faceIndex] | (1 << faceVariation);
        return true;
    }

    public bool faceCategoryHasUnlocks(int faceIndex)
    {
        return unlockedFaces[faceIndex] > 0;
    }

    public int currentEquippedFaceAbsoluteIndex(int faceIndex)
    {
        if (faceIndex == 0)
            return 0;

        int relativeIndex = 0;
        int absoluteIndex = 0;

        for (int i = 1; i < unlockedFaces.Length; i++)
        {
            absoluteIndex++;
            if (unlockedFaces[i] > 0)
                relativeIndex++;

            if (faceIndex == relativeIndex)
                return absoluteIndex;
        }

        return 100;
    }

}
