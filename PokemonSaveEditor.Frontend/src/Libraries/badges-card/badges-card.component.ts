import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';
import { Badge } from './models/badge';

import badgesData from '../../assets/badges.json';

@Component({
  selector: 'app-badges-card',
  templateUrl: './badges-card.component.html',
  styleUrls: ['./badges-card.component.css']
})

export class BadgesCardComponent implements OnInit {

  //badges: Badge[] = [];

  //private badgeSubject = new BehaviorSubject<Badge[]>([]);
  //private _jsonBadgesURL = 'assets/badges.json';

  //badges$: Observable<Badge[]> = new Observable;
  badges:{type:String,icon:String}[]=badgesData;

  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
    {
      //this.badges$ = this.httpClient.get<Badge[]>("../../assets/badges.json");
    }
  }

}
