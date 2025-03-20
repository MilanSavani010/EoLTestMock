import React, { useState } from "react";
import "../../styles/UtilComponents/List.css";

// Define the props interface with generics
interface ListProps<T> {
  items: T[]; // Array of items of any type
  getKey: (item: T) => string | number; // Function to extract a unique key
  renderContent: (item: T) => React.ReactNode; // Function to render item content
}

const List = <T,>({ items, getKey, renderContent }: ListProps<T>) => {
  // State for selected item key (string | number | null)
  const [selectedItemKey, setSelectedItemKey] = useState<string | number | null>(null);
  // State for the list items, initialized with props.items
  const [listItems, setListItems] = useState<T[]>(items);

  // Handle double-click to select an item
  const handleDoubleClick = (item: T) => {
    const key = getKey(item);
    setSelectedItemKey(key);
  };

  // Handle deletion of an item
  const handleDelete = (item: T) => {
    const key = getKey(item);
    setListItems((prevItems) => prevItems.filter((i) => getKey(i) !== key));
    if (selectedItemKey === key) {
      setSelectedItemKey(null); // Clear selection if deleted item was selected
    }
  };

  // Sync listItems with props.items if the parent updates the list
  React.useEffect(() => {
    setListItems(items);
  }, [items]);

  return (
    <div className="list-container">
      <ul className="list">
        {listItems.map((item) => {
          const key = getKey(item); // Extract key for each item
          return (
            <li
              key={key} // Use the extracted key as the React key
              className={`list-item ${selectedItemKey === key ? "active" : ""}`}
              onDoubleClick={() => handleDoubleClick(item)}
            >
              <span className="list-item-content">{renderContent(item)}</span>
              <button
                className="delete-button"
                onClick={(e) => {
                  e.stopPropagation(); // Prevent triggering double-click
                  handleDelete(item);
                }}
              >
                ✕
              </button>
            </li>
          );
        })}
      </ul>
    </div>
  );
};

export default List;