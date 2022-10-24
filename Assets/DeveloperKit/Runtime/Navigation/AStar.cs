using System.Collections.Generic;
using UnityEngine;

namespace DeveloperKit.Runtime.Navigation
{
    public abstract class AStar<T> where  T : INode
    {
        //开表，已经计算过cost值的节点
        protected List<INode> open = new List<INode>();
        //闭表，已经经过的节点
        protected List<INode> close = new List<INode>();
        //起点
        private INode _start;
        //终点
        private INode _end;

        /// <summary>
        /// 获取相邻节点
        /// </summary>
        protected abstract List<INode> GetAdjacentNode(INode node);

        /// <summary>
        /// 获取位置所在的节点
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        protected abstract INode GetNodeByPosition(Vector3 position);

        /// <summary>
        /// 计算相邻点的代价
        /// </summary>
        /// <param name="node"></param>
        /// <param name="list"></param>
        protected abstract void CalculateAdjacentNodeCost(INode node, List<INode> list);

        protected abstract void AddCloseNode(INode node);

        protected abstract void AddRangeOpenNode(List<INode> nodeList);

        /// <summary>
        /// 获取开表中最小cost的节点
        /// </summary>
        /// <returns></returns>
        private INode GetMinCostNode()
        {
            open.Sort((a,b) => a.Cost < b.Cost ? 1 : -1 );
            return open[^1];
        }

        /// <summary>
        /// 评估，计算可行路径
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public bool Evaluate(Vector3 from,Vector3 to)
        {
            open.Clear();
            close.Clear();
            _start = GetNodeByPosition(from);
            _end = GetNodeByPosition(to);
            open.Add(_start);
            while (open.Count > 0)
            {
                var node = GetMinCostNode();
                if (node.Id == _end.Id)
                {
                    return true;
                }
                var adjacentNode = GetAdjacentNode(node);
                CalculateAdjacentNodeCost(node, adjacentNode);
                AddCloseNode(node);
                AddRangeOpenNode(adjacentNode);
            }
            return false;
        }
    }

    public interface INode
    {
        public Vector3Int Id { get; set; }
        public float ToStart { get; set; }
        public float ToEnd { get; set; }
        public float Cost => ToEnd + ToStart;
        
        public INode Parent { get; set; }

        public void SetParent(INode parent)
        {
            Parent = parent;
        }
    }

}