
// DIT WORDT GEBRUIKT ALS WE ONZE STATE OOK IN DE LOCALSTORAGE WILLEN OPSLAAN

// De state terug in de React-Redux applicatie inladen
export const loadState = () => {
  try {
    const serializedState = localStorage.getItem("state");
    if (serializedState === null) {
      return undefined;
    }
    return JSON.parse(serializedState);
  } catch (err) {
    console.log("Error in loading the state ", err);
    return undefined;
  }
};

// De state in de React-Redux applicatie opslaan
export const saveState = (state) => {
  try {
    const serializedState = JSON.stringify(state);
    localStorage.setItem("state", serializedState);
  } catch (err) {
    console.log("Error in saving the state ", err);
  }
};
