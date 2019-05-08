using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace ToDoList.Tests
{
  [TestClass]
  public class ItemTest : IDisposable
  {

    public void Dispose()
    {
      Item.ClearAll();
    }

    public ItemTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=ToDoListTest;";
    }

    [TestMethod]
    public void ItemConstructor_CreatesInstanceOfItem_Item()
    {
      Item newItem = new Item("test", 1);
      Assert.AreEqual(typeof(Item), newItem.GetType());
    }

    [TestMethod]
    public void GetDescription_ReturnsDescription_String()
    {
      string description = "Walk the dog.";
      Item newItem = new Item(description, 1);
      string result = newItem.GetDescription();
      Assert.AreEqual(description, result);
    }

    [TestMethod]
    public void SetDescription_SetDescription_String()
    {
      string description = "Walk the dog.";
      Item newItem = new Item(description, 1);
      string updatedDescription = "Do the dishes";
      newItem.SetDescription(updatedDescription);
      string result = newItem.GetDescription();
      Assert.AreEqual(updatedDescription, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyList_ItemList()
    {
      List<Item> newList = new List<Item> ();
      List<Item> result = Item.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Item()
    {
      Item firstItem = new Item("Mow the lawn", 1);
      Item secondItem = new Item("Mow the lawn", 1);
      Assert.AreEqual(firstItem, secondItem);
    }

    [TestMethod]
  public void Save_SavesToDatabase_ItemList()
  {
    Item testItem = new Item("Mow the lawn", 1);
    testItem.Save();
    List<Item> result = Item.GetAll();
    List<Item> testList = new List<Item>{testItem};
    CollectionAssert.AreEqual(testList, result);
  }
    //
    [TestMethod]
    public void GetAll_ReturnsItems_ItemList()
    {
      //Arrange
      string description01 = "Walk the dog";
      string description02 = "Wash the dishes";
      Item newItem1 = new Item(description01, 1);
      newItem1.Save();
      Item newItem2 = new Item(description02, 1);
      newItem2.Save();
      List<Item> newList = new List<Item> { newItem1, newItem2 };

      //Act
      List<Item> result = Item.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn", 1);

      //Act
      testItem.Save();
      Item savedItem = Item.GetAll()[0];

      int result = savedItem.GetId();
      int testId = testItem.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectItemFromDatabase_Item()
    {
      //Arrange
      Item testItem = new Item("Mow the lawn", 1);
      testItem.Save();

      //Act
      Item foundItem = Item.Find(testItem.GetId());

      //Assert
      Assert.AreEqual(testItem, foundItem);
    }

      [TestMethod]
    public void Edit_UpdatesItemInDatabase_String()
    {
      //Arrange
      string firstDescription = "Walk the Dog";
      Item testItem = new Item(firstDescription, 1);
      testItem.Save();
      string secondDescription = "Mow the lawn";

      //Act
      testItem.Edit(secondDescription);
      string result = Item.Find(testItem.GetId()).GetDescription();

      //Assert
      Assert.AreEqual(secondDescription, result);
    }

    [TestMethod]
    public void GetCategoryId_ReturnsItemsParentCategoryId_Int()
    {
      Category newCategory = new Category("Home Tasks");
      Item newItem = new Item("Walk the dog.", 1, newCategory.GetId());
      int result = newItem.GetCategoryId();
      Assert.AreEqual(newCategory.GetId(), result);
    }

    //
    // [TestMethod]
    // public void GetId_ItemsInstantiateWithAnIdAndGetterReturns_Int()
    // {
    //   //Arrange
    //   string description = "Walk the dog.";
    //   Item newItem = new Item(description);
    //
    //   //Act
    //   int result = newItem.GetId();
    //
    //   //Assert
    //   Assert.AreEqual(1, result);
    // }
    //
    // [TestMethod]
    // public void Find_ReturnsCorrectItem_Item()
    // {
    //   //Arrange
    //   string description01 = "Walk the dog";
    //   string description02 = "Wash the dishes";
    //   Item newItem1 = new Item(description01);
    //   Item newItem2 = new Item(description02);
    //
    //   //Act
    //   Item result = Item.Find(2);
    //
    //   //Assert
    //   Assert.AreEqual(newItem2, result);
    // }

  }
}
