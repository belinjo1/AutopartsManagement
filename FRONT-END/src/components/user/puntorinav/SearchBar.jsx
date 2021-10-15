//import { Input } from 'semantic-ui-react';
import { Search } from "semantic-ui-react";
import React from 'react';

export const SearchBar = (props) => {

    return (
        <Search
            fluid
            icon="search"
            placeholder={props.placeholder}
            onSearchChange={props.handleChange}
            open={false}
            />
    )

}
export default SearchBar;


