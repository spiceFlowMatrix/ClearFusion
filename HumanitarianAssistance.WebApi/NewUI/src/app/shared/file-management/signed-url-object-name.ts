import { FileSourceEntityTypes } from '../enum';

 export class SignedUrlObjectName {
  static getSignedURLObjectName(pageId: number, entityId?: number): string {
    let objectName = '';

    switch (pageId) {
      case FileSourceEntityTypes.Voucher:
      objectName = 'voucher/';
      break;
      case FileSourceEntityTypes.StorePurchase:
      objectName = 'storepurchase/' + entityId + '/';
      break;
      case FileSourceEntityTypes.Employee:
      objectName = 'employeeprofileimage/' + entityId + '/';
      break;
      case FileSourceEntityTypes.ComparativeStatement:
      objectName = 'comparativeStatement/' + entityId + '/';
      break;
      case FileSourceEntityTypes.HiringRequestCandidateCV:
      objectName = 'HiringRequestCandidateCV/' + entityId + '/';
      break;
    }

    return objectName;
  }
 }
