Imports CoflexWeb.CoflexWeb.Services.Web
Imports System.Collections.Generic
Public Class ItemComp
    Implements IHierarchyData

    Private _categoryId As Integer
    Private _parentId As Integer
    Private _name As String

    Public Property CategoryId() As Integer
        Get
            Return _categoryId
        End Get
        Set
            _categoryId = Value
        End Set
    End Property

    Public Property ParentId() As Integer
        Get
            Return _parentId
        End Get
        Set
            _parentId = Value
        End Set
    End Property
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set
            _name = Value
        End Set
    End Property

    Public Sub New(categoryId As Integer, parentId As Integer, name As String)
        _categoryId = categoryId
        _parentId = parentId
        _name = name
    End Sub

    Public Function GetChildren() As IHierarchicalEnumerable Implements IHierarchyData.GetChildren
        ' Call to the local cache for the data
        Dim children As New ItemsComponentsCollection()
        ' Loop through your local data and find any children
        For x = 0 To CoflexWebServices.itemsLists.Count - 1
            Dim item = CoflexWebServices.itemsLists.Item(x)
            If item.ParentId = Me.CategoryId Then

                Dim newT1 As ItemComp = DirectCast(DirectCast(item, Object), ItemComp)


                children.Add(newT1)
            End If
        Next
        Return children
    End Function


    Public Function GetParent() As IHierarchyData Implements IHierarchyData.GetParent
        ' Loop through your local data and report back with the parent
        For x = 0 To CoflexWebServices.itemsLists.Count
            Dim item = CoflexWebServices.itemsLists.Item(x)
            If item.CategoryId = Me.ParentId Then
                Return item
            End If
        Next
        Return Nothing
    End Function

    Public ReadOnly Property HasChildren As Boolean Implements IHierarchyData.HasChildren
        Get
            Dim children As ItemsComponentsCollection = TryCast(GetChildren(), ItemsComponentsCollection)
            Return children.Count > 0
        End Get
    End Property

    Public ReadOnly Property Item As Object Implements IHierarchyData.Item
        Get
            Return Me
        End Get
    End Property

    Public ReadOnly Property Path As String Implements IHierarchyData.Path
        Get
            Return Me.CategoryId.ToString
        End Get
    End Property

    Public ReadOnly Property Type As String Implements IHierarchyData.Type
        Get
            Return Me.GetType.ToString
        End Get
    End Property
End Class
Public Class ItemsComponentsCollection
    Inherits List(Of ItemComp)
    Implements IHierarchicalEnumerable

    Public Sub New()
        MyBase.New()
    End Sub
    Public Function GetHierarchyData(enumeratedItem As Object) As IHierarchyData Implements IHierarchicalEnumerable.GetHierarchyData
        Return enumeratedItem
    End Function
End Class

