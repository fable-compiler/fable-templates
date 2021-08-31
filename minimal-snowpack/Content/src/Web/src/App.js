import { createAtom } from "./.fable/fable-library.3.2.12/Util.js";
import { printf, toText } from "./.fable/fable-library.3.2.12/String.js";

export let count = createAtom(0);

export const myButton = document.querySelector(".my-button");

myButton.onclick = ((_arg1) => {
    let arg10;
    count(count() + 1, true);
    myButton.innerText = ((arg10 = (count() | 0), toText(printf("You clicked: %i time(s)"))(arg10)));
});

