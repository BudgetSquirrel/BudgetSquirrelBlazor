using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetSquirrel.Common.AggregationUtils
{
  public static class TreeAggregationUtils
  {
    /// <summary>
    /// Recursively loads the given relationship for each node in the tree. Returns
    /// the list of relationships in a flat list.
    /// </summary>
    public static async Task<IEnumerable<TRelationship>> SelectAsync<TRelationship, TTreeNode>(
      TTreeNode treeRoot,
      Func<TTreeNode, IEnumerable<TTreeNode>> childrenSelector,
      Func<TTreeNode, Task<TRelationship>> fetchRelationship)
    {

      // Load the relationship for the current node in the tree.
      Task<TRelationship> rootLoadTask = fetchRelationship(treeRoot);

      // Recursively load relationships for each child of the current fund branch.
      IEnumerable<TRelationship> allRelationshipsInBranches = await FetchRelationshipsForChildren(treeRoot, childrenSelector, fetchRelationship);

      IEnumerable<TRelationship> allRelationships = allRelationshipsInBranches.Prepend(await rootLoadTask);

      return allRelationshipsInBranches;
    }

    private static async Task<IEnumerable<TRelationship>> FetchRelationshipsForChildren<TRelationship, TTreeNode>(TTreeNode treeRoot, Func<TTreeNode, IEnumerable<TTreeNode>> childrenSelector, Func<TTreeNode, Task<TRelationship>> fetchRelationship)
    {
      List<Task<IEnumerable<TRelationship>>> childLoadTasks = new List<Task<IEnumerable<TRelationship>>>();
      IEnumerable<TTreeNode> branches = childrenSelector(treeRoot);
      foreach (TTreeNode branch in branches)
      {
        childLoadTasks.Add(TreeAggregationUtils.SelectAsync(branch, childrenSelector, fetchRelationship));
      }
      await Task.WhenAll(childLoadTasks);

      List<TRelationship> budgetsInTree = new List<TRelationship>();
      foreach (Task<IEnumerable<TRelationship>> childLoad in childLoadTasks)
      {
        budgetsInTree.AddRange(await childLoad);
      }

      return budgetsInTree;
    }
  }
}
