using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding<T>
{
    public delegate bool Satisfies(T item);
    public delegate List<T> GetNeighbours(T item);
    public List<T> GetPath(T start, Satisfies satisfies, GetNeighbours getNeighbours, int watchdog = 500)
    {
        HashSet<T> visited = new HashSet<T>();
        Stack<T> pending = new Stack<T>();
        Dictionary<T, T> parents = new Dictionary<T, T>();

        pending.Push(start);
        while (pending.Count != 0)
        {
            T current = pending.Pop();
            if (satisfies(current))
            {
                return BuildPath(current, parents);
            }
            visited.Add(current);
            List<T> neighbours = getNeighbours(current);
            for (int i = 0; i < neighbours.Count; i++)
            {
                var currNeighbour = neighbours[i];
                if (visited.Contains(currNeighbour) || pending.Contains(currNeighbour)) continue;
                pending.Push(currNeighbour);
                parents[currNeighbour] = current;
            }
            watchdog--;
            if (watchdog <= 0)
            {
                break;
            }
        }
        return new List<T>();
    }

    List<T> BuildPath(T end, Dictionary<T, T> parents)
    {
        List<T> path = new List<T>();
        path.Add(end);
        while (parents.ContainsKey(path[path.Count - 1]))
        {
            path.Add(parents[path[path.Count - 1]]);
        }
        path.Reverse();
        return path;
    }
}
