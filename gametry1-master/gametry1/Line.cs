namespace gametry1
{
    public class Index
    {
        public Index()
        {
            m_x = 0;
            m_y = 0;
        }

        public Index(int x, int y)
        {
            m_x = x;
            m_y = y;
        }

        public Index(Index r)
        {
            m_x = r.m_x;
            m_y = r.m_y;
        }

        public int X { get { return m_x; } set { m_x = value; } }
        public int Y { get { return m_y; } set { m_y = value; } }

        int m_x, m_y;
    }
    internal class Line
    {
        public Line()
        {
            m_start = new Index();
            m_finish = new Index();
        }

        public Line(Index start, Index finish)
        {
            m_start = new Index(start);
            m_finish = new Index(finish);
        }

        public Index Start { get { return m_start; } set { m_start = new Index(value); } }
        public Index Finish { get { return m_finish; } set { m_finish = new Index(value); } }

        Index m_start;
        Index m_finish;
    }
}