import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DbheaderComponent } from './dbheader.component';

describe('DbheaderComponent', () => {
  let component: DbheaderComponent;
  let fixture: ComponentFixture<DbheaderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DbheaderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DbheaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
