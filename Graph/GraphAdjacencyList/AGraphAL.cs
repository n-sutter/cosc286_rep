﻿using Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphAdjacencyList
{
    public abstract class AGraphAL<T> : AGraph<T> where T : IComparable<T>
    {
        #region Attributes

        protected List<List<Edge<T>>> listListEdges;
        
        #endregion

        public AGraphAL()
        {
            listListEdges = new List<List<Edge<T>>>();
        }

        public override void AddVertexAdjustEdges(Vertex<T> v)
        {
            //add a new list to the end of the lisst of lists
            listListEdges.Add(new List<Edge<T>>());
        }

        public override IEnumerable<Vertex<T>> EnumerateNeighbors(T data)
        {
            List<Vertex<T>> neighbours = new List<Vertex<T>>();
            //what row are the edges for data in
            //Stored as an index in the corresponding index containing data
            Vertex<T> v = GetVertex(data);
            //get the sublist
            List<Edge<T>> subList = listListEdges[v.Index];
            //loop trhough all of the neighbours, and add them as a vertex
            //object to the list
            for (int c = 0; c < subList.Count; c++)
            {
                //if teh current location is an edge
                if (subList[c] != null)
                {
                    //add the "to" vertex to the list
                    neighbours.Add(subList[c].To);
                }
            }

            return neighbours;
        }

        public override Edge<T> GetEdge(T from, T to)
        {
            if (!HasEdge(from, to))
            {
                throw new ApplicationException("No such edge");
            }
            //index in and return the edge object
            return listListEdges[GetVertex(from).Index][GetVertex(to).Index];
        }

        public override bool HasEdge(T from, T to)
        {
            //get the vertex objrcts for from and to
            Vertex<T> vFrom = GetVertex(from);
            Vertex<T> vTo = GetVertex(to);
            //get the list of edges for "from" vertex
            List<Edge<T>> al = listListEdges[vFrom.Index];

            return al.Contains(new Edge<T>(vFrom, vTo));

        }

        public override void RemoveEdge(T from, T to)
        {
            if (HasEdge(from, to))
            {
                //Index in to matrix and set location to null
                listListEdges[GetVertex(from).Index][GetVertex(to).Index] = null;
                numEdges--;
            }
            else
            {
                throw new ApplicationException("Edge not found");
            }
        }

        public override void RemoveVertexAdjustEdges(Vertex<T> v)
        {
            throw new NotImplementedException();
        }

        public override void AddEdge(Edge<T> e)
        {
            //if the edge already exists, throw an exception
            if(HasEdge(e.From.Data, e.To.Data))
            {
                throw new ApplicationException("Edge already exists");
            }

            //add an edge to the list of edges for the "from" vertex
            listListEdges[e.From.Index].Add(e);
            //increment the edge count
            numEdges++;

        }

        public override Edge<T>[] getAllEdges()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            StringBuilder sbEdges = new StringBuilder("Edges:\n");
            for (int r = 0; r < listListEdges.Count; r++)
            {
                sbEdges.Append("Index " + r + ": ");
                bool commaAdded = false;
                foreach (Edge<T> e in listListEdges[r])
                {
                    sbEdges.Append(e + ", ");
                    commaAdded = true;
                }
                if (commaAdded)
                {
                    sbEdges.Remove(sbEdges.Length - 2, 2);
                }
                sbEdges.Append("\n");
            }
            return base.ToString() + sbEdges;
        }
    }
}
