import React, { Component } from 'react';
import './VesselListControl.css';

export class VesselListControl extends Component {
    static displayName = VesselListControl.Name;

    constructor(props) {
        super(props);
        this.state = {
            onVesselSelectionChange: props.onSelectVessel,
            list: []
        };
        this.timer = this.timer.bind(this);

        setInterval(this.timer, 5000);
        this.vesselHeaderClicked = this.vesselHeaderClicked.bind(this);
        this.getVesselId = this.getVesselId.bind(this);
    }

    timer() {
        fetch('https://localhost:5001/api/vessels/headers')
            .then(response => response.json())
            .then(json => this.setState({ list: json }))
            .catch(error => alert(error));
    }

    render() {
        return (
            <div>
                <div>{this.state.list.length} users.</div>
                <select size="20"
                    id="vesselSelector"
                    className="vessel-selector" onChange={this.vesselHeaderClicked}>
                    {this.state.list.map((vessel, index) =>
                        <option
                            key={vessel.mmsi}
                            value={vessel.mmsi} >{this.getVesselId(vessel)}</option>
                    )}
                </select>
            </div>
        );
    }

    getVesselId(vessel) {
        if (vessel.hasOwnProperty('name'))
            return vessel.name + ' (MMSI ' + vessel.mmsi + ')';

        return 'MMSI ' + vessel.mmsi ;
    }

    vesselHeaderClicked() {
        var vesselSelector = document.getElementById('vesselSelector');
        var selectedMMSI = vesselSelector.options[vesselSelector.selectedIndex].value;
        this.state.onVesselSelectionChange(selectedMMSI);
    }
}