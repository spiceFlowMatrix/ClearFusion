import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DbsidebarComponent } from './dbsidebar.component';

describe('DbsidebarComponent', () => {
  let component: DbsidebarComponent;
  let fixture: ComponentFixture<DbsidebarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DbsidebarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DbsidebarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
