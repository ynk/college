
import './App.css';
import Header from "./componenten/Header";
import Part from "./componenten/Part";
import List from "./componenten/List";
import Footer from "./componenten/Footer";

const App = () => {
  const course = "Web 3";
  const part1 = "Introduction";
  const exercies1 = 15;
  const part2 = "ES Syntax";
  const exercies2 = 14;

  const technologies = [{id: 1, name: "Node.js"},{id: 2, name: "JavaScript"},{id: 3, name: "Express"},{id: 4, name: "React"}];





  return (
    <div className="text">
      <Header course={course}/>
      <Part part={part1} exercise={exercies1}/>
      <Part part={part2} exercise={exercies2}/>
      <List technologies={technologies}/>
      <Footer exercise={exercies1 + exercies2}/>
    </div>
  );
}

export default App;
