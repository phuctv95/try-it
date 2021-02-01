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
- All React components must act like pure functions with respect to their *props* (not try to edit props).

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

Some life cycle:
- The `componentDidMount()` method runs after the component output has been rendered to the DOM.
- The `componentWillUnmount()` method will called whenever the component output is removed from the DOM.

The data flows down:
- This is commonly called a “top-down” or “unidirectional” data flow. Any state is always owned by some specific component, and any data or UI derived from that state can only affect components “below” them in the tree.

Handling events:
- You have to be careful about the meaning of this in JSX callbacks. In JavaScript, class methods are not *bound* by default. If you forget to bind `this.handleClick` and pass it to `onClick`, this will be `undefined` when the function is actually called.
- You can use an arrow function in the callback.
- The problem with this syntax is that a different callback is created each time the component's output renders. In most cases, this is fine. However, if this callback is passed as a prop to lower components, those components might do an extra re-rendering. We generally recommend binding in the constructor or using the class fields syntax, to avoid this sort of performance problem.

Keys:
- Keys help React identify which items have changed, are added, or are removed. 
- Keys only make sense in the context of the surrounding array. E.g. if you extract a `ListItem` component, you should keep the key on the `<ListItem />` elements in the array rather than on the `<li>` element in the `ListItem` itself.
- A good rule of thumb is that elements inside the `map()` call need keys.
- Keys serve as a hint to React but they don’t get passed to your components. If you need the same value in your component, pass it explicitly as a prop with a different name.

Forms:
- Controlled components: handle the submission of the form and has access to the data that the user entered into the form.
- Controlled components handle the changes by component's state, and only update it with `setState()`.
- Read more about `textarea`, `select` in https://reactjs.org/docs/forms.html

Thinking in React example:
- From a mock: ![](thinking-in-react-mock.png)
- Step 1: Break The UI Into A Component Hierarchy
    + The first thing you’ll want to do is to draw boxes around every component (and subcomponent) in the mock and give them all names.
    + A component should ideally only do one thing (single responsibility principle).
    + It shoulk looks like this: ![](thinking-in-react-components.png)
        - FilterableProductTable (orange): contains the entirety of the example
        - SearchBar (blue): receives all user input
        - ProductTable (green): displays and filters the data collection based on user input
        - ProductCategoryRow (turquoise): displays a heading for each category
        - ProductRow (red): displays a row for each product
    + Final:
        ```
        FilterableProductTable
            + SearchBar
            + ProductTable
                - ProductCategoryRow
                - ProductRow
        ```
- Step 2: Build A Static Version in React
    + The easiest way is to build a version that takes your data model and renders the UI but has no interactivity.
    + Because this step is bulding a static version, we only use `props`, not `state`.
    + You can build top-down or bottom-up.
    + The components will only have render() methods.
- Step 3: Identify The Minimal (but complete) Representation Of UI State
    + The key here is DRY: Don’t Repeat Yourself. Figure out the absolute minimal representation of the state your application needs and compute everything else you need on-demand.
    + We have in this example:
        - The original list of products
        - The search text the user has entered
        - The value of the checkbox
        - The filtered list of products
    + Ask three questions about each piece of data:
        - Is it passed in from a parent via props? If so, it probably isn’t state.
        - Does it remain unchanged over time? If so, it probably isn’t state.
        - Can you compute it based on any other state or props in your component? If so, it isn’t state.
    + The original list of products is passed in as props, so that’s not state. The search text and the checkbox seem to be state since they change over time and can’t be computed from anything. And finally, the filtered list of products isn’t state because it can be computed by combining the original list of products with the search text and value of the checkbox.
    + So finally, our state is:
        - The search text the user has entered
        - The value of the checkbox
- Step 4: Identify Where Your State Should Live
    + For each piece of state in your application:
        - Identify every component that renders something based on that state.
        - Find a common owner component (a single component above all the components that need the state in the hierarchy).
        - Either the common owner or another component higher up in the hierarchy should own the state.
        - If you can’t find a component where it makes sense to own the state, create a new component solely for holding the state and add it somewhere in the hierarchy above the common owner component.
    + In our example:
        - `ProductTable` needs to filter the product list based on state and `SearchBar` needs to display the search text and checked state.
        - The common owner component is `FilterableProductTable`.
        - It conceptually makes sense for the filter text and checked value to live in `FilterableProductTable`
- Step 5: Add Inverse Data Flow
    + The form components deep in the hierarchy need to update the state in FilterableProductTable.
    + Since components should only update their own state, `FilterableProductTable` will pass callbacks to `SearchBar` that will fire whenever the state should be updated.

Others:
- To prevent components from rendering, just return `null`.
- Lift state up:
    + In React, sharing state is accomplished by moving it up to the closest common ancestor of the components that need it.
    + Example in docs: instead of reading `this.state.temperature`, we now read `this.props.temperature`. Instead of calling `this.setState()` when we want to make a change, we now call `this.props.onTemperatureChange(`), which will be provided by the `Calculator`.
    + There should be a single “source of truth” for any data that changes in a React application.
- Nesting component will assign it to `props.children`.
- React has a powerful composition model, and we recommend using composition instead of inheritance to reuse code between components.
- `props` vs `state`: `props` get passed to the component (similar to function parameters) whereas `state` is managed within the component (similar to variables declared within a function).
