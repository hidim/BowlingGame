
/// <summary>
/// Bowling Game Core Worker class. 
/// </summary>
public class BowlingGame
{
    private int _frameCounter;
    private int _frameHitCounter;
    private int[,] _hits;
    private int[,,] _finalHit;
    private int _score = 0;

    public BowlingGame()
    {
        // Initialize Game's Default Values
        _frameCounter = 0;
        _frameHitCounter = 0;
        _hits = new int[9, 2] { { -1, -1 }, { -1, -1 }, { -1, -1 }, { -1, -1 }, { -1, -1 }, { -1, -1 }, { -1, -1 }, { -1, -1 }, { -1, -1 } };
        _finalHit = new int[1, 1, 3] { { { -1, -1, -1 } } };
    }

    public void Play(int pins)
    {
        // Validation of frame numbers!
        if (_frameHitCounter == 1 && _frameCounter < 9)
        {
            var prevHit = _hits[_frameCounter, 0];
            if (pins > 10 - prevHit)
            {
                Console.WriteLine("You cannot hit pins more than {0}!", 10 - prevHit);
                return;
            }
        }

        // if last hit is 10th frame?
        if (_frameCounter >= 9)
        {
            _finalHit[0, 0, _frameHitCounter] = pins;
            _frameHitCounter++;
            _frameCounter = 10;
        }
        else
        {
            // Define hit on frame
            _hits[_frameCounter, _frameHitCounter] = pins;

            if (_frameHitCounter == 1)
            {
                _frameCounter++;
                _frameHitCounter = 0;
            }
            else
            {
                _frameHitCounter = 1;
            }
        }


        CalculateScore();

        // Update counter for every hit to calculate frames and indexes.

    }

    private void CalculateScore()
    {
        _score = 0;
        int _strikePoint = 0;

        // Calculating normal score
        for (int i = 0; i < _frameCounter; i++)
        {
            if (i == 9) // 10th frame
            {
                if (_strikePoint > 0)
                {
                    _score += (_strikePoint * 10) + 10;
                    _strikePoint = 0;
                }

                // 10th frame calculation
                if (_finalHit[0, 0, 0] != -1 && _finalHit[0, 0, 1] != -1 && _finalHit[0, 0, 2] != -1)
                {
                    _score += _finalHit[0, 0, 0] + _finalHit[0, 0, 1] + _finalHit[0, 0, 2];
                }
            }
            else
            {
                // Normal Hit
                if (_hits[i, 0] + _hits[i, 1] < 10)
                {
                    _score += (_strikePoint * 10) + _hits[i, 0] + _hits[i, 1];

                    if (_strikePoint > 0)
                    {
                        _score += _hits[i, 0] + _hits[i, 1];
                    }

                    _strikePoint = _strikePoint > 0 ? 0 : _strikePoint;
                }
                // Strike Hit
                else if (_hits[i, 0] == 10)
                {
                    _strikePoint++;
                }
                // Spare Hit
                else if (_hits[i, 0] + _hits[i, 1] == 10 && _hits[i + 1, 0] > 0)
                {
                    _score += (_strikePoint * 10) + 10 + _hits[i + 1, 0];

                    _strikePoint = _strikePoint > 0 ? 0 : _strikePoint;
                }
            }
        }
    }

    public int Score()
    {
        return _score;
    }
}