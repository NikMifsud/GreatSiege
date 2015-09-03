using System.Collections.Generic;
//interface that should be implemented by grid nodes used in E. Lippert's generic path finding implementation
//pathing script, do not modify

public interface IHasNeighbours<N>
{
	IEnumerable<N> Neighbours { get; }
}