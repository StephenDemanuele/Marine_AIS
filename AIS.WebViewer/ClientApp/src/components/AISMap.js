import { ReactBingmaps } from 'react-bingmaps';
import React, { Component } from 'react';
import { VesselListControl } from './VesselListControl';
import './Map.css';

export class AISMap extends Component {
    static displayName = Map.name;

    constructor(props) {
        super(props);
        this.state =
            {
                key: "AoU0wkIqbJAtSwkGYKXvFm2tIQgBbBwRMYezOupR6UI2QmkmAC3lD-Zq0C6pQioO",
                infoboxes: []
            };
        this.timer = this.timer.bind(this);
        setInterval(this.timer, 2000);
    }

    render() {
        return (
            <div>
                <ReactBingmaps
                    id="myMap"
                    bingmapKey={this.state.key}
                    className="map-one"
                    center={[35.9918, 14.5075]}
                    mapTypeId={"aerial"}
                    infoboxesWithPushPins={this.state.infoboxes}
                    >
                </ReactBingmaps>
                <VesselListControl onSelectVessel={this.onSelectVesselHandler} />
            </div>
        );
    }
    onSelectVesselHandler(args) {
        alert("parentFunc says " + args);
    }

    timer() {
        fetch('https://localhost:5001/api/vessels/details')
            .then(response => response.json())
            .then(vessels => {
                var _infoboxes = vessels.map(function (val, index, arr) {
                    return {
                        'id': val.mmsi,
                        'location': [val.Lat, val.Lon],
                        'addHandler': 'mouseover',
                        'infoboxOption': {
                            title: val.Id,
                            description: 'Dir:' + val.Heading + ' SOG:' + val.sog + ' ' + val.Type + '. Updated ' + val.lastUpdateDescription
                            },
                        'pushPinOption': {
                            title: val.Id,
                            color: val.sog > 0 ? 'green' : 'red',
                            description: 'Dir:' + val.Heading + ' SOG:' + val.sog + ' ' + val.Type
                            }
                        }
                });
                this.setState({ infoboxes: _infoboxes })
            })
            .catch(error => alert(error));
    }
}