using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPTreeApp.Distance
{
    /// <summary>
    /// This implementation is based on Magnus Lie Hetland's Python code (http://hetland.org/coding/python/levenshtein.py)
    /// </summary>
    public class LevenshteinDistance : IDistance<int, string>
    {
        public int calculateDistance(string input, string otherInput)
        {
            int len = input.Length;
            int otherLen = otherInput.Length;
            if (len > otherLen)
            {
                int tempLen = len;
                len = otherLen;
                otherLen = tempLen;
                string tempInput = input;
                input = otherInput;
                otherInput = tempInput;
            }
            List<int> current = Enumerable.Range(0, len + 1).ToList<int>();
            for (int idx = 1; idx <= otherLen; idx++)
            {
                var previous = current;
                current = Enumerable.Repeat(0, len + 1).ToList<int>();
                current[0] = idx;
                for (int subIdx = 1; subIdx <= len; subIdx++)
                {
                    int forInsertion = previous[subIdx] + 1;
                    int forDeletion = current[subIdx - 1] + 1;
                    int forSubtitution = previous[subIdx - 1];
                    if (input[subIdx - 1] != otherInput[idx - 1])
                    {
                        forSubtitution++;
                    }
                    current[subIdx] = Math.Min(Math.Min(forInsertion, forDeletion), forSubtitution);
                }
            }
            return current[len];
        }
    }
}
