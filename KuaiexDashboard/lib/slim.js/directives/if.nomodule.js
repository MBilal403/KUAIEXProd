Slim.customDirective(e=>"s:if"===e.nodeName,(e,t,i)=>{let l=i.value,o=l,a=!1;"!"===o.charAt(0)&&(o=o.slice(1),a=!0);let m;const r=document.createComment(`{$target.localName} if:${l}`);t.parentNode.insertBefore(r,t);Slim.bind(e,t,o,()=>{let i=!!Slim.lookup(e,o,t);a&&(i=!i),i!==m&&(i?(t.__isSlim&&t.createdCallback(),r.parentNode.insertBefore(t,r.nextSibling)):Slim.removeChild(t),m=i)})},!0);