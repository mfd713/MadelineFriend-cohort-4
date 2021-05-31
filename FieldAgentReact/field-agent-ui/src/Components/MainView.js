import {useState, useEffect} from 'react';
import {Switch, Route} from 'react-router-dom';
import AddAgent from './AddAgent';
import Home from './Home';

    function MainView() {

        return(
        <Switch> {/* The Switch decides which component to show based on the current URL.*/}
            <Route exact path='/' component={Home}></Route>
            <Route exact path='/add' component={AddAgent}></Route>
        </Switch>
        )
        }

        export default MainView;

