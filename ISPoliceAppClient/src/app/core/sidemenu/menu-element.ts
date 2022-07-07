export const menus = [
  {
    name: 'Dashboard',
    icon: 'dashboard',
    link: false,
    open: false,
    sub: [
      {
        name: 'Input Entry',
        icon: 'input',
        link: '/auth/dashboard-input',
        open: false,
      },
      {
        name: 'Dashboard2',
        icon: 'source',
        link: '/auth/dashboard-input',
        open: false,
      },
      {
        name: 'Dashboard3',
        icon: 'campaign',
        link: '/auth/dashboard-input',
        open: false,
      },
    ],
  },
  {
    name: 'Masters',
    icon: 'category',
    link: false,
    open: false,
    sub: [
      {
        name: 'venue',
        icon: 'location_on',
        link: '/auth/venue',
        open: false,
      },
      {
        name: 'Venue Permission Type',
        icon: 'where_to_vote',
        link: '/auth/vptype',
        open: false,
      },
      {
        name: 'control-room',
        icon: 'where_to_vote',
        link: '/auth/control-room',
        open: false,
      },
      {
        name: 'Gender',
        icon: 'gender',
        link: '/auth/global/gender-list',
        open: false,
      },
      {
        name: 'Marital Status',
        icon: 'status',
        link: '/auth/global/marital-list',
        open: false,
      },
      {
        name: 'Religion',
        icon: 'church',
        link: '/auth/global/religion-list',
        open: false,
        
      }   
    ],
  },
  {
    name: 'Search',
    icon: 'search',
    link: false,
    open: false,
    sub: [
      {
        name: 'Keyword Search',
        icon: 'search',
        link: '/auth/search',
        open: false,
      },
      {
        name: 'Advanced Search',
        icon: 'saved_search',
        link: '/auth/advanced-search',
        open: false,
      },
    ],
  },
  {
    name: 'Profile',
    icon: 'person',
    link: '/auth/profile-list',
    open: false,
  },
  {
    name: 'Person',
    icon: 'person',
    link: '/auth/person',
    open: false,
  },
  {
    name: 'Organization',
    icon: 'category',
    link: false,
    open: false,
    sub: [
      {
        name: 'New Org',
         icon: 'category',
        link: '/auth/organization/org-list',
        open: false,
      },
      {
        name: 'Sub Org',
         icon: 'category',
         link: '/auth/global/sub-organization-list',
         open: false,
      },
      {
        name: 'Assign Events',
         icon: 'category',
         link: '/auth/organization/events',
         open: false,
      },     
      {
        name: 'Leaders',
        icon: 'people',
        link: '/auth/leaders/list',
        open: false,
        
      }
      
     
    ]
  },
  {
    name: 'Allegation',
    icon: 'category',
    link: false,
    open: false,
    sub: [
    
      {
        name: 'Personnel',
        icon: 'person',
        link: '/auth/personnel/personnel-list',
        open: false,
      },
      {
        name: 'Allegation',
        icon: 'layers',
        link:  '/auth/allegation/allegation-list',
        open: false,
        sub: [
          {
            name: 'Enquiry',
            icon: 'notes',
            link: '/auth/enquiry/enquiry-list',
            open: false,
          },         
        ],
      }
    ]
  },
  {
    name: 'Allegation Enquiry',
    icon: 'category',
    link:  false,
    open: false,
    sub: [
      {
        name: 'Enquiry',
        icon: 'library_books',
        link: '/auth/enquiry/enquiry-list',
        open: false,
       },
       {
        name: 'Reports',
        icon: 'category',
        link: false,
         open: false,
         sub: [
          {
            name: 'Notes',
            icon: 'library_books',
            link: '/auth/reports/notes-list',
            open: false,
          },
          {
            name: 'Memorandum',
            icon: 'library_books',
            link: '/auth/reports/memorandum-list',
            open: false,
          },
           
          ]
       },
    
    ],
  }

  
 
];
