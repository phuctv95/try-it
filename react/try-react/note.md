## Basic

Note from video https://www.youtube.com/watch?v=x7cQ3mrcKaY:
- *Coupling* is the degree to which each program module relies on each of the other module.
- *Cohension* is the degree to which elements of a module belong together.
- Display logic and markup are inevitably tighly coupled and highly cohensive.
- Use *React components* for highly cohensive building block for UIs loosely coupled with other components.
- Only put display logic in your *components*.
- When data changes, React re-renders entire component.
- Re-rendering on every change makes things simple.
- The re-rendering process is done by a virtual DOM: build a new virtual DOM, compare with the old one, update the new changes.

Basic:
- React embraces the fact that rendering logic is inherently coupled with other UI logic.
- Declare a HTML element in JSX is compiled to be calling `React.createElement()`, it means that's just a normal object, these object are called *React elements*.
- These objects are what you want to see on the screen. React reads these objects and uses them to construct the DOM and keep it up to date.

Rendering elements:
- React elements are immutable. Once you create an element, you can’t change its children or attributes.
- With our knowledge so far, the only way to update the UI is to create a new element, and pass it to ReactDOM.render().
- Most React apps only call ReactDOM.render() once.

Components:
- A component returns a React element describing what should appear on the screen.
- Declare component by:
    + using function:
        ```js
        function Welcome(props) {
            return <h1>Hello, {props.name}</h1>;
        }
        ```
    + or using class:
        ```js
        class Welcome extends React.Component {
            render() {
                return <h1>Hello, {this.props.name}</h1>;
            }
        }
        ```
- Props using:
    ```js
    ReactDOM.render(<Welcome name="Leo"/>, document.getElementById('root'));
    ```
- All React components must act like pure functions with respect to their props (not try to edit props).

State:
- `setState()` checks state changes and call `render()` to show up the new changes.
- Do not assign this.state manually, only do this in constructor.
- Because this.props and this.state may be updated asynchronously, you should not rely on their values for calculating the next state. To fix it, use a second form of setState() that accepts a function rather than an object.
- State Updates are Merged:
    ```js
    this.state = {
      posts: [],
      comments: []
    };
    this.setState({ posts: response.posts });
    // are merged with:
    this.setState({ comments: response.comments });
    ```

The data flows down:
- This is commonly called a “top-down” or “unidirectional” data flow. Any state is always owned by some specific component, and any data or UI derived from that state can only affect components “below” them in the tree.
