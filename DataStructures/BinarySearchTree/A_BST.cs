﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStructuresCommon;

namespace BinarySearchTree
{
    public abstract class A_BST<T> : A_Collection<T>, I_BST<T> where T : IComparable<T>
    {
        #region Attributes

        //a pointer to the root node 
        protected Node<T> nRoot;
        //A counter to keep track of the number of data items
        //in the tree
        protected int iCount;

        public override int Count
        {
            get
            {
                return iCount;
            }
        }

        #endregion

        #region I_BST Implementation

        public abstract T Find(T data);
       
            public abstract int Height();

            public abstract void Iterate(ProcessData<T> pd, TRAVERSALORDER to);

        #endregion

        
    }
}
